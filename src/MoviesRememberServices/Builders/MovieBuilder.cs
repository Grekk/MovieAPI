using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberDomain;

namespace MoviesRememberServices.Builders
{
    public class MovieBuilder : IMovieBuilder
    {
        public TinyMovie BuildTinyMovie(dynamic jsonData)
        {
            TinyMovie movie = new TinyMovie();
            movie.ApiId = BuildProperty(jsonData, "code");
            movie.PictureUrl = BuildProperty(jsonData, "poster", "href");
            movie.Title = BuildProperty(jsonData, "title");
            movie.OriginalTitle = BuildProperty(jsonData, "originalTitle");
            movie.PressRatings = BuildProperty(jsonData, "statistics", "pressRating");
            movie.UserRatings = BuildProperty(jsonData, "statistics", "userRating");
            movie.Actors = BuildProperty(jsonData, "castingShort", "actors");
            movie.Director = BuildProperty(jsonData, "castingShort", "directors");

            string releaseString = BuildProperty(jsonData, "release", "releaseDate");
            DateTime releaseDate;
            if (DateTime.TryParse(releaseString, out releaseDate))
            {
                movie.ReleaseDate = releaseDate;
            }

            movie.Trailer = BuildProperty(jsonData, "trailer", "href");

            return movie;
        }

        public Movie BuildMovie(dynamic jsonData)
        {
            Movie movie = new Movie();
            movie.ApiId = jsonData.code;

            try { movie.PictureUrl = jsonData.poster.href; }
            catch { movie.PictureUrl = null; }

            try { movie.Title = jsonData.title; }
            catch { movie.Title = null; }

            try { movie.OriginalTitle = jsonData.originalTitle; }
            catch { movie.OriginalTitle = null; }

            try { movie.Actors = jsonData.castingShort.actors; }
            catch { movie.Actors = null; }

            try { movie.Director = jsonData.castingShort.directors; }
            catch { movie.Director = null; }

            try
            {
                string releaseString = jsonData.release.releaseDate;
                DateTime releaseDate;
                if (DateTime.TryParse(releaseString, out releaseDate))
                {
                    movie.ReleaseDate = releaseDate;
                }
            }
            catch
            {
            }

            try
            {
                movie.Trailer = BuildProperty(jsonData.trailer, "href");
            }
            catch { }

            try
            {
                movie.LinkList = BuildLink(jsonData.link);
            }
            catch { }

            try { movie.PressRatings = jsonData.statistics.pressRating; }
            catch { movie.PressRatings = null; }

            try { movie.UserRatings = jsonData.statistics.userRating; }
            catch { movie.UserRatings = null; }

            try { movie.Synopsis = jsonData.synopsis; }
            catch { movie.Synopsis = null; }

            return movie;
        }

        public IList<Link> BuildLink(dynamic jsonData)
        {
            IList<Link> result = new List<Link>();
            try
            {
                foreach (dynamic item in jsonData)
                {
                    result.Add(new Link() { Name = item["name"], Href = item["href"] });
                }
            }
            catch
            {
            }

            return result;
        }

        private dynamic BuildProperty(dynamic jsonData, string firstKey, string secondKey)
        {
            dynamic result = null;
            try
            {
                result = (jsonData[firstKey])[secondKey];
            }
            catch
            {
            }

            return result;
        }

        private dynamic BuildProperty(dynamic jsonData, string firstKey)
        {
            dynamic result = null;
            try
            {
                result = jsonData[firstKey];
            }
            catch
            {
            }

            return result;
        }
    }
}
