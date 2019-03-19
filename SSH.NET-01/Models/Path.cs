using System;
using System.Collections.Generic;

namespace SSH.NET_01.Models
{
    public partial class Path
    {
        public int IdPath { get; set; }
        public string StrPath { get; set; }
        public string StrContent { get; set; }
        public string StrScraper { get; set; }
        public string StrHash { get; set; }
        public int? ScanRecursive { get; set; }
        public sbyte? UseFolderNames { get; set; }
        public string StrSettings { get; set; }
        public sbyte? NoUpdate { get; set; }
        public sbyte? Exclude { get; set; }
        public string DateAdded { get; set; }
        public int? IdParentPath { get; set; }
    }
}
