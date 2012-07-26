using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberServices.Interface;
using MoviesRememberDomain;
using MoviesRememberServices.Utils;
using System.Web.Script.Serialization;
using MoviesRememberServices.Builders;

namespace MoviesRememberServices
{
    public class SearchService : ISearchService
    {
        private JavaScriptSerializer _jss;
        private readonly IMovieBuilder _movieBuilder;


        public SearchService(IMovieBuilder movieBuilder)
        {
            _jss = new JavaScriptSerializer();
            _jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            _movieBuilder = movieBuilder;
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
    }
}
