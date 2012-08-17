using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MoviesRememberServices.Utils;
using MoviesRememberDomain;

namespace MovieAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    public class UserService : IUserService
    {
        private readonly MoviesRememberServices.Interface.IUserService _userService;

        public UserService()
        {
            _userService = Bootstrapper.GetInstance<MoviesRememberServices.Interface.IUserService>();
        }

        public void AddMovie(Guid userId, string userName, MoviesRememberDomain.Movie movie)
        {
            _userService.AddMovie(userId, userName, movie);
        }

        public void DeleteMovie(string userName, MoviesRememberDomain.UserMovie movie)
        {
            _userService.DeleteMovie(userName, movie);
        }

        public void UpdateMovie(MoviesRememberDomain.UserMovie movie)
        {
            _userService.UpdateMovie(movie);
        }

        public TinyUserMovieList GetUserMovieList(Guid userId, int numPage)
        {
            return _userService.GetUserMovieList(userId, numPage);
        }

        public IList<MoviesRememberDomain.UserAction> AddUserAction(MoviesRememberDomain.UserAction action)
        {
            return _userService.AddUserAction(action);
        }

        public IList<MoviesRememberDomain.UserAction> GetUsersActions()
        {
            return _userService.GetUsersActions();
        }

        public bool AddNewMember(string email)
        {
            return _userService.AddNewMember(email);
        }
    }
}
