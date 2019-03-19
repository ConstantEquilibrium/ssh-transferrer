using System;
using System.Collections.Generic;

namespace SSH.NET_01.Models
{
    public partial class Bookmark
    {
        public int IdBookmark { get; set; }
        public int? IdFile { get; set; }
        public double? TimeInSeconds { get; set; }
        public double? TotalTimeInSeconds { get; set; }
        public string ThumbNailImage { get; set; }
        public string Player { get; set; }
        public string PlayerState { get; set; }
        public int? Type { get; set; }
    }
}
