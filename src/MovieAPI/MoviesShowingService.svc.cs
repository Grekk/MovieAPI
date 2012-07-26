using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MoviesRememberDomain;
using MoviesRememberServices.Utils;
using StructureMap;

namespace MovieAPI
{
    public class MoviesShowingService : IMoviesShowingService
    {
        private readonly MoviesRememberServices.Interface.IMoviesShowingService _moviesShowingService;

        public MoviesShowingService()
        {
            _moviesShowingService = Bootstrapper.GetInstance<MoviesRememberServices.Interface.IMoviesShowingService>();
        }

        public TinyMovieList GetNowShowingMoviesByRate(int numPage)
        {
            return _moviesShowingService.GetNowShowingMoviesByRate(numPage);
        }

        public TinyMovieList GetNowShowingMoviesByDate(int numPage)
        {
            return _moviesShowingService.GetNowShowingMoviesByDate(numPage);
        }

        public TinyMovieList GetComingSoonMoviesByRate(int numPage)
        {
            return _moviesShowingService.GetComingSoonMoviesByRate(numPage);
        }

        public TinyMovieList GetComingSoonMoviesByDate(int numPage)
        {
            return _moviesShowingService.GetComingSoonMoviesByDate(numPage);
        }

        public IList<TinyMovie> GetBestWeekMovies()
        {
            return _moviesShowingService.GetBestWeekMovies();
        }

        public Movie GetMovie(long code)
        {
            return _moviesShowingService.GetMovie(code);
        }
    }
}
