using System;
using System.Collections.Generic;

namespace SSH.NET_01.Models
{
    public partial class Files
    {
        public int IdFile { get; set; }
        public int? IdPath { get; set; }
        public string StrFilename { get; set; }
        public int? PlayCount { get; set; }
        public string LastPlayed { get; set; }
        public string DateAdded { get; set; }
    }
}
