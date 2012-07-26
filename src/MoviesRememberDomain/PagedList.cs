using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MoviesRememberDomain
{
    [DataContract]
    public class PagedList<T>
    {
        public PagedList()
        {
            EntityList = new List<T>();
            CurrentPage = 1;
            TotalResult = 0;
            TotalPage = 1;
        }

        [DataMember]
        public IList<T> EntityList { get; set; }

        [DataMember]
        public int CurrentPage { get; set; }
        private int _count = 24;
        [DataMember]
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

        [DataMember]
        public int TotalResult { get; set; }
        [DataMember]
        public int TotalPage { get; set; }
    }
}
