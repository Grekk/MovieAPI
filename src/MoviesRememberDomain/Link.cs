using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MoviesRememberDomain
{
    public class Link
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Href { get; set; }
    }
}
