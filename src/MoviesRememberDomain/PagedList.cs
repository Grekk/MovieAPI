using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MoviesRememberDomain
{
    [DataContract]
    public class PagedList<T>
    {
        private int _count = int.Parse(ConfigurationManager.AppSettings["MovieCountByPage"]);


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
