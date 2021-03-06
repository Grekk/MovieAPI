﻿using System;
using System.Collections.Generic;
using MoviesRememberDomain;

namespace MoviesRememberServices.Interface
{
    public interface IUserService
    {
        void AddMovie(Guid userId, string userName, Movie movie);

        void DeleteMovie(string userName, UserMovie movie);

        void UpdateMovie(UserMovie movie);

        TinyUserMovieList GetUserMovieList(Guid userId, int numPage);

        IList<UserAction> AddUserAction(UserAction action);

        IList<UserAction> GetUsersActions();

        bool AddNewMember(string email);
    }
}
