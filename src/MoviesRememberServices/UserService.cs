using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using MoviesRememberServices.Interface;
using MoviesRememberDomain;
using AutoMapper;
using MoviesRememberDao;
using MoviesRememberDao.Interface;
using MoviesRememberDB;
using MoviesRememberServices.Utils;
using RestSharp;
using Action = MoviesRememberDomain.Action;

namespace MoviesRememberServices
{
    public class UserService : IUserService
    {
        private readonly AbstractUserMovieDAO _userMovieRepo;
        private readonly IUserActionsDAO _userActionDAO;
        private readonly IMoviesShowingService _moviesService;


        public const int UserActionsLength = 50;


        public UserService(IUserActionsDAO userActionDAO, AbstractUserMovieDAO userMovieRepo, IMoviesShowingService moviesService)
        {
            _userMovieRepo = userMovieRepo;
            _userActionDAO = userActionDAO;
            _moviesService = moviesService;
        }

        public void AddMovie(Guid userId, string userName, Movie movie)
        {
            TinyMovie tinyMovie = (TinyMovie)movie;
            user_movie userMovie = Mapper.Map<TinyMovie, user_movie>(tinyMovie);
            userMovie.user_movie_user_id = userId;

            _userMovieRepo.Insert(userMovie);

            UserAction actionToAdd = new UserAction(userName, movie);
            actionToAdd.Action = Action.ADD_MOVIE;
            AddUserAction(actionToAdd);
        }

        public void DeleteMovie(string userName, UserMovie movie)
        {
            _userMovieRepo.DeleteById(movie.Id);

            UserAction actionToAdd = new UserAction(userName, movie);
            actionToAdd.Action = Action.DELETE_MOVIE;
            AddUserAction(actionToAdd);
        }

        public void UpdateMovie(UserMovie userMovie)
        {
            user_movie userMovieToUpdate = Mapper.Map<UserMovie, user_movie>(userMovie);
            _userMovieRepo.Update(userMovieToUpdate);
        }

        public IList<UserMovie> GetUserMovieList(Guid userId)
        {
            new LogEvent("Log ok").Raise();
            Bootstrapper.Bootstrap();
            IList<UserMovie> result = new List<UserMovie>();
            IList<user_movie> dbResult = _userMovieRepo.GetByUserId(userId);
            foreach (user_movie movie in dbResult)
            {
                result.Add(Mapper.Map<user_movie, UserMovie>(movie));
            }

            return result;
        }

        public IList<UserAction> AddUserAction(UserAction action)
        {
            IList<UserAction> userActions = new List<UserAction>();

            try
            {
                userActions = _userActionDAO.AddActionAtFirstIndex(action);
                if (userActions.Count == UserActionsLength)
                {
                    userActions = _userActionDAO.RemoveLastAction();
                }

                return userActions;
            }
            catch (Exception ex)
            {
                new LogEvent(ex.Message).Raise();
            }

            return userActions;
        }

        public IList<UserAction> GetUsersActions()
        {
            return _userActionDAO.GetActions();
        }

        public bool AddNewMember(string email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               ConfigurationManager.AppSettings["MAILGUN_API_KEY"]);
            RestRequest request = new RestRequest();
            request.Resource = "lists/{list}/members";
            request.AddParameter("list", ConfigurationManager.AppSettings["MAILING_LIST"], ParameterType.UrlSegment);
            request.AddParameter("address", email);
            request.AddParameter("subscribed", true);
            request.Method = Method.POST;
            IRestResponse response = client.Execute(request);
            return response.ResponseStatus == ResponseStatus.Completed;
        }

        public void SendMoviesReleased()
        {
            new LogEvent("Envoyé !!").Raise();
            string url = ConfigurationManager.AppSettings["MOVIE_URL"];
            string htmlContent = "<html><body><p>Voici la liste des films conseillés qui sortent aujourd'hui:</p><section>";

            foreach (TinyMovie movie in _moviesService.GetBestWeekMovies())
            {
                htmlContent += "<section style=\"width: 200px;height: 500px;float: left;padding: 10px;\">";
                htmlContent += "<a href=\"" + url + movie.ApiId + "\"><img src=\"" + movie.PictureUrl + "\" height=\"193\" width=\"143\"/></a>";
                htmlContent += "</section>";
            }

            htmlContent += "</section></body></html>";

            SendMessage(htmlContent);

        }

        private void SendMessage(string message)
        {
            string mess = "Send Message :" + ConfigurationManager.AppSettings["MAILGUN_API_KEY"] + " : " +
                          ConfigurationManager.AppSettings["MAIL_DOMAIN"] + " : " +
                          ConfigurationManager.AppSettings["MAILING_LIST"];

            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               ConfigurationManager.AppSettings["MAILGUN_API_KEY"]);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 ConfigurationManager.AppSettings["MAIL_DOMAIN"], ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "movies.remember@movies.fr");
            request.AddParameter("to", ConfigurationManager.AppSettings["MAILING_LIST"]);
            request.AddParameter("subject", "Sortie ciné");
            request.AddParameter("html", message);
            request.Method = Method.POST;
            IRestResponse response = client.Execute(request);
            if (response.ErrorException != null)
            {
                new LogEvent(response.ErrorMessage).Raise();
            }
        }
    }
}
