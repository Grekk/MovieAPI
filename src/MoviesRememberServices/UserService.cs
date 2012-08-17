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
using MoviesRememberServices.MailService;
using MoviesRememberServices.Utils;
using ServiceStack.ServiceInterface.ServiceModel;
using Action = MoviesRememberDomain.Action;

namespace MoviesRememberServices
{
    public class UserService : IUserService
    {
        private readonly AbstractUserMovieDAO _userMovieRepo;
        private readonly IUserActionsDAO _userActionDAO;
        private readonly IMoviesShowingService _moviesService;
        private readonly IMailService _mailService;


        public const int UserActionsLength = 50;


        public UserService(IUserActionsDAO userActionDAO, AbstractUserMovieDAO userMovieRepo, IMoviesShowingService moviesService, IMailService mailService)
        {
            _userMovieRepo = userMovieRepo;
            _userActionDAO = userActionDAO;
            _moviesService = moviesService;
            _mailService = mailService;
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

        public TinyUserMovieList GetUserMovieList(Guid userId, int numPage)
        {
            return _userMovieRepo.GetByUserId(userId, numPage);
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
            string mailingList = ConfigurationManager.AppSettings["MAILING_LIST"];
            return _mailService.AddNewUser(mailingList, email);
        }
    }
}
