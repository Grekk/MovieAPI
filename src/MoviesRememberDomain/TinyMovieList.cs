﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MoviesRememberDomain
{
    public class TinyMovieList
    {
        public TinyMovieList()
        {
            TinyMovies = new PagedList<TinyMovie>();
        }

        [DataMember]
        public PagedList<TinyMovie> TinyMovies { get; set; }

        [DataMember]
        public double NbWeek
        {
            get
            {
                return Math.Round((TinyMovies.EntityList.Where(x => x.ReleaseDate.HasValue).ToList().Max(x => x.ReleaseDate.Value - DateTime.Now)).TotalDays / 7, 0);
            }
        }

        [DataMember]
        public int CurrentWeek { get; set; }

        [DataMember]
        public List<TinyMovie> GetMovieByWeek
        {
            get
            {
                DateTime start = DateTime.Today.AddDays(CurrentWeek * 7);
                DateTime end = start.AddDays(7);
                return TinyMovies.EntityList.Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value <= end && m.ReleaseDate.Value > start).ToList();
            }
        }
    }

    public enum OrderEnum
    {
        BY_DATE,
        BY_RANK
    }
}