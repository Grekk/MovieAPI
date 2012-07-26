using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberDomain;

namespace MoviesRememberServices.Interface
{
    public interface IMoviesShowingService
    {
        TinyMovieList GetNowShowingMoviesByRate(int numPage);
        TinyMovieList GetNowShowingMoviesByDate(int numPage);

        TinyMovieList GetComingSoonMoviesByRate(int numPage);
        TinyMovieList GetComingSoonMoviesByDate(int numPage);

        IList<TinyMovie> GetBestWeekMovies();

        Movie GetMovie(long code);
    }
}
