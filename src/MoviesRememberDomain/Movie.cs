using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MoviesRememberDomain
{
    [DataContract]
    public class Movie : TinyMovie
    {
        public Movie()
        {
            LinkList = new List<Link>();
        }

        [DataMember]
        public IList<Link> LinkList { get; set; }

        [DataMember]
        public string Synopsis { get; set; }
    }
}
