using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MoviesRememberDomain;
using MoviesRememberServices.Utils;

namespace MovieAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SearchService" in code, svc and config file together.
    public class SearchService : ISearchService
    {
        private readonly MoviesRememberServices.Interface.ISearchService _searchService;

        public SearchService()
        {
            _searchService = Bootstrapper.GetInstance<MoviesRememberServices.Interface.ISearchService>();
        }

        public TinyMovieList Search(string query)
        {
            return _searchService.Search(query);
        }
    }
}
