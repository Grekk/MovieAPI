using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MoviesRememberDomain
{
    public class UserMovie
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string PictureUrl { get; set; }

        [Display(Name = "Date de sortie: ")]
        public string ReleaseDate { get; set; }

        public long ApiId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name = "Note: ")]
        public int? Rate { get; set; }

        [Display(Name = "Supprimer? ")]
        public bool ShouldDelete { get; set; }
    }
}
