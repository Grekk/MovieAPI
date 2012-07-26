using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesRememberDomain
{
    public class UserAction
    {
        public UserAction()
        {
        }

        public UserAction(string userName, Movie movie)
        {
            UserName = userName;
            MovieName = movie.Title;
            MovieId = movie.ApiId.ToString();
        }

        public UserAction(string userName, UserMovie movie)
        {
            UserName = userName;
            MovieName = movie.Title;
            MovieId = movie.ApiId.ToString();
        }

        public string UserName { get; set; }
        public Action Action { get; set; }
        public string MovieName { get; set; }
        public string MovieId { get; set; }
    }

    public enum Action
    {
        ADD_MOVIE,
        DELETE_MOVIE,
        SHOW_MOVIE
    }
}
