using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSH.NET_01.Models
{
    public class MovieList
    {
        public List<MovieModel> Movies { get; set; }
    }

    public class MovieModel
    {
        public SftpFile File { get; set; }
        public Movie MovieData { get; set; }
    }
}
