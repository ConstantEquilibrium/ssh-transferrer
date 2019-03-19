using System;
using System.Collections.Generic;

namespace SSH.NET_01.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int? MediaId { get; set; }
        public string MediaType { get; set; }
        public string RatingType { get; set; }
        public float? Rating1 { get; set; }
        public int? Votes { get; set; }
    }
}
