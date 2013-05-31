using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberServices.Interface;
using MoviesRememberDomain;
using System.Web.Script.Serialization;
using System.Net;
using MoviesRememberServices.Utils;
using MoviesRememberServices.Builders;

namespace MoviesRememberServices
{
    public class MoviesShowingService : IMoviesShowingService
    {
        private JavaScriptSerializer _jss;
        private readonly IMovieBuilder _movieBuilder;

        public MoviesShowingService(IMovieBuilder movieBuilder)
        {
            _jss = new JavaScriptSerializer();
            _jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            _movieBuilder = movieBuilder;
        }

        public TinyMovieList GetNowShowingMoviesByRate(int numPage)
        {
            TinyMovieList result = new TinyMovieList();

            string json = JsonUtils.GetJson(Properties.Resources.TOP_RANKED_MOVIES_NOW_SHOWING + numPage);
            dynamic glossaryEntry = _jss.Deserialize(json, typeof(object)) as dynamic;

            TinyMovie movie = null;
            foreach (dynamic value in glossaryEntry.feed.movie)
            {
                movie = _movieBuilder.BuildTinyMovie(value);
                result.TinyMovies.EntityList.Add(movie);
            }

            SetPaging(result, glossaryEntry.feed);

            return result;
        }

        public TinyMovieList GetNowShowingMoviesByDate(int numPage)
        {
            TinyMovieList result = new TinyMovieList();

            string json = JsonUtils.GetJson(Properties.Resources.ORDER_BY_DATE_MOVIES_NOW_SHOWING + numPage);
            dynamic glossaryEntry = _jss.Deserialize(json, typeof(object)) as dynamic;

            TinyMovie movie = null;
            foreach (dynamic value in glossaryEntry.feed.movie)
            {
                movie = _movieBuilder.BuildTinyMovie(value);
                result.TinyMovies.EntityList.Add(movie);
            }

            SetPaging(result, glossaryEntry.feed);

            result.TinyMovies.EntityList = result.TinyMovies.EntityList.OrderByDescending(x => x.ReleaseDate).ToList();

            return result;
        }

        public TinyMovieList GetComingSoonMoviesByRate(int numPage)
        {
            TinyMovieList result = new TinyMovieList();

            string json = JsonUtils.GetJson(Properties.Resources.TOP_RANKED_MOVIES_COMING_SOON + numPage);
            dynamic glossaryEntry = _jss.Deserialize(json, typeof(object)) as dynamic;

            TinyMovie movie = null;
            foreach (dynamic value in glossaryEntry.feed.movie)
            {
                movie = _movieBuilder.BuildTinyMovie(value);
                result.TinyMovies.EntityList.Add(movie);
            }

            SetPaging(result, glossaryEntry.feed);

            return result;
        }

        public TinyMovieList GetComingSoonMoviesByDate(int numPage)
        {
            TinyMovieList result = new TinyMovieList();

            string json = JsonUtils.GetJson(Properties.Resources.ORDER_BY_DATE_MOVIES_COMING_SOON + numPage);
            dynamic glossaryEntry = _jss.Deserialize(json, typeof(object)) as dynamic;
            TinyMovie movie = null;
            foreach (dynamic value in glossaryEntry.feed.movie)
            {
                movie = _movieBuilder.BuildTinyMovie(value);
                result.TinyMovies.EntityList.Add(movie);
            }
            result.TinyMovies.EntityList = result.TinyMovies.EntityList.OrderBy(x => x.ReleaseDate).ToList();
            SetPaging(result, glossaryEntry.feed);

            return result;
        }

        public IList<TinyMovie> GetBestWeekMovies()
        {
            TinyMovieList movieListByDate = GetNowShowingMoviesByDate(1);
            TinyMovieList movieListByRate = GetNowShowingMoviesByRate(1);
            IList<TinyMovie> topMoviesRateList = new List<TinyMovie>();
            IList<TinyMovie> resultMovieList = new List<TinyMovie>();
            DateTime? maxDateTime = movieListByDate.TinyMovies.EntityList.Where(x => x.ReleaseDate.Value.DayOfWeek == DayOfWeek.Wednesday).Max(x => x.ReleaseDate);


            for (int i = 0; i < 10; i++)
            {
                topMoviesRateList.Add(movieListByRate.TinyMovies.EntityList[i]);
            }

            foreach (TinyMovie movie in movieListByDate.TinyMovies.EntityList.Where(x => x.ReleaseDate == maxDateTime))
            {
                if (movie.PressRatings >= 3 || topMoviesRateList.Where(r => r.ApiId == movie.ApiId).SingleOrDefault() != null)
                {
                    resultMovieList.Add(movie);
                }
            }

            return resultMovieList;
        }

        public TinyMovieList Search(string query)
        {
            TinyMovieList result = new TinyMovieList();

            string json = JsonUtils.GetJson(Properties.Resources.SEARCH_MOVIE_URL + query);
            dynamic glossaryEntry = _jss.Deserialize(json, typeof(object)) as dynamic;

            TinyMovie movie = null;
            try
            {
                foreach (dynamic value in glossaryEntry.feed.movie)
                {
                    movie = _movieBuilder.BuildTinyMovie(value);
                    result.TinyMovies.EntityList.Add(movie);
                }
            }
            catch
            {
            }

            return result;
        }

        public Movie GetMovie(long code)
        {
            string json = JsonUtils.GetJson(Properties.Resources.DISPLAY_MOVIE_URL + "&code=" + code);
            dynamic glossaryEntry = _jss.Deserialize(json, typeof(object)) as dynamic;
            return _movieBuilder.BuildMovie(glossaryEntry.movie);
        }

        private void SetPaging(TinyMovieList movieList, dynamic feed)
        {
            movieList.TinyMovies.TotalResult = (int)feed.totalResults;
        }


    }
}
