using System;
using System.Collections.Generic;

namespace SSH.NET_01.Models
{
    public partial class Art
    {
        public int ArtId { get; set; }
        public int? MediaId { get; set; }
        public string MediaType { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
