using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MoviesRememberDomain
{
    [DataContract]
    public class UserMovie
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string PictureUrl { get; set; }

        [DataMember]
        [Display(Name = "Date de sortie: ")]
        public string ReleaseDate { get; set; }

        [DataMember]
        public long ApiId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        [Display(Name = "Note: ")]
        public float? Rate { get; set; }

        [DataMember]
        [Display(Name = "Supprimer? ")]
        public bool ShouldDelete { get; set; }
    }
}
