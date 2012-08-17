using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MoviesRememberDomain
{
    [DataContract]
    public class TinyUserMovieList
    {
        public TinyUserMovieList()
        {
            TinyUserMovies = new PagedList<UserMovie>();
        }

        [DataMember]
        public PagedList<UserMovie> TinyUserMovies { get; set; }
    }

}
