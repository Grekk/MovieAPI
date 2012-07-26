using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesRememberDomain
{
    public class PagedList<T>
    {
        public PagedList()
        {
            EntityList = new List<T>();
            CurrentPage = 1;
            TotalResult = 0;
            TotalPage = 1;
        }

        public IList<T> EntityList { get; set; }

        public int CurrentPage { get; set; }
        private int _count = 24;
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }

        public int TotalResult { get; set; }
        public int TotalPage { get; set; }
    }
}
