using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MoviesRememberDomain
{
    [DataContract]
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

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public Action Action { get; set; }

        [DataMember]
        public string MovieName { get; set; }

        [DataMember]
        public string MovieId { get; set; }
    }

    [DataContract]
    public enum Action
    {
        [EnumMember]
        ADD_MOVIE,
        [EnumMember]
        DELETE_MOVIE,
        [EnumMember]
        SHOW_MOVIE
    }
}
