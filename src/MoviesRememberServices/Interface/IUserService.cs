﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberDomain;
using RestSharp;

namespace MoviesRememberServices.Interface
{
    public interface IUserService
    {
        void AddMovie(Guid userId, string userName, Movie movie);

        void DeleteMovie(string userName, UserMovie movie);

        void UpdateMovie(UserMovie movie);

        IList<UserMovie> GetUserMovieList(Guid userId);

        IList<UserAction> AddUserAction(UserAction action);

        IList<UserAction> GetUsersActions();

        bool AddNewMember(string email);

        void SendMoviesReleased();
    }
}
