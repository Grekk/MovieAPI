using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberDomain;

namespace MoviesRememberServices.Builders
{
    public interface IMovieBuilder
    {
        TinyMovie BuildTinyMovie(dynamic jsonData);

        Movie BuildMovie(dynamic jsonData);
    }
}
