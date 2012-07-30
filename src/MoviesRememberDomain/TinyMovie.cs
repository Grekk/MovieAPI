using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MoviesRememberDomain
{
    [DataContract]
    public class TinyMovie
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public long ApiId { get; set; }

        [DataMember]
        public string PictureUrl { get; set; }

        [DataMember]
        public string Title { get; set; }

        private string _originalTitle = string.Empty;

        [DataMember]
        public string OriginalTitle
        {
            get
            {
                return  string.IsNullOrEmpty(Title) || _originalTitle.ToLowerInvariant() != Title.ToLowerInvariant() ? _originalTitle : string.Empty;
            }
            set
            {
                _originalTitle = value;
            }
        }

        [DataMember]
        public string Actors { get; set; }

        [DataMember]
        public string Director { get; set; }

        private decimal? _userRatings;
        [DataMember]
        public decimal? UserRatings
        {
            get
            {
                return _userRatings;
            }
            set
            {
                if(value != null)
                {
                    value = decimal.Round(value.Value, 2);
                }

                _userRatings = value;
            }
        }

        private decimal? _pressRatings;
        [DataMember]
        public decimal? PressRatings
        {
            get
            {
                return _pressRatings;
            }
            set
            {
                if (value != null)
                {
                    value = decimal.Round(value.Value, 2);
                }

                _pressRatings = value;
            }
        }

        [DataMember]
        public string Trailer { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ReleaseDate { get; set; }
    }
}
