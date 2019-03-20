using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Renci.SshNet;
using SSH.NET_01.Models;
using Microsoft.Extensions.Configuration;

namespace SSH.NET_01.Controllers
{
    public class HomeController : Controller
    {
        private KodiContext _context;
        public IConfiguration Configuration { get; }

        public HomeController(IConfiguration configuration, KodiContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        public async Task<List<MovieModel>> GetMovies()
        {
            var dbList = _context.Movie.ToList();
            foreach (var item in dbList)
            {
                item.C00 = Regex.Replace(item.C00, "[^0-9a-zA-Z ]+", "");
            }

            var remoteConf = Configuration.GetSection("Remote");

            var remote = new { username = remoteConf.GetSection("username").Value, password = remoteConf.GetSection("password").Value, address = remoteConf.GetSection("address").Value };

            var movies = new List<MovieModel>();
            var remoteConn = new ConnectionInfo(remote.address, remote.username, new PasswordAuthenticationMethod(remote.username, remote.password));
            using (var client = new SftpClient(remoteConn))
            {
                client.Connect();

                var dir = client.ListDirectory("downloads/movies");

                foreach (var item in dir)
                {
                    if (item.Name != ".." && item.Name != ".")
                    {
                        var cleanName = Regex.Replace(item.Name, "[^0-9a-zA-Z ]+", "");

                        var title = cleanName.Substring(0, cleanName.Length - 5);
                        var year = cleanName.Substring(cleanName.Length - 4);

                        var Data = dbList.SingleOrDefault(x => x.C00.ToLower().Equals(title.ToLower()) && x.Premiered.Substring(0, 4).Equals(year));

                        if (Data == null)
                        {
                            Data = new Movie();
                        } else
                        {
                            var thumbList = Data.C08.Split("aspect=\"poster\" preview=\"");
                            var thumb = thumbList[1].Substring(0, 62);
                            Data.C08 = thumb;
                        }

                        movies.Add(new MovieModel
                        {
                            File = item,
                            MovieData = Data
                        });
                    }
                }
            }

            return movies;
        }

        public async Task<IActionResult> Download(string name)
        {
            var remoteConf = Configuration.GetSection("Remote");
            var localConf = Configuration.GetSection("Local");

            var remote = new { username = remoteConf.GetSection("username").Value, password = remoteConf.GetSection("password").Value, address = remoteConf.GetSection("address").Value }; // UPDATE INFO
            var local = new { username = localConf.GetSection("username").Value, password = localConf.GetSection("password").Value, address = localConf.GetSection("address").Value };

            var destination = "/media/movies";
            var source = "downloads/movies/";

            var remoteConn = new ConnectionInfo(remote.address, remote.username, new PasswordAuthenticationMethod(remote.username, remote.password));
            var localConn = new ConnectionInfo(local.address, local.username, new PasswordAuthenticationMethod(local.username, local.password));

            using (var connection = new SshClient(localConn))
            {
                connection.Connect();
                var res = connection.RunCommand("cd /media/movies/ && ls");
                string[] movieList = res.Result.Split('\n');

                using (var client = new SftpClient(remoteConn))
                {
                    client.Connect();
                    var dir = client.ListDirectory(source);

                    var tasks = new List<Task>();

                    foreach (var item in dir)
                    {
                        if (item.Name.ToLower().Equals(name.ToLower()))
                        {
                            tasks.Add(DownloadFileAsync(client, item.FullName, destination + "\\" + item.Name));
                        }
                        //if (item.Name.Contains("S01E01"))
                        //{
                        //    tasks.Add(DownloadFileAsync(client, item.FullName, destination + "\\" + item.Name));
                        //}
                    }

                    await Task.WhenAll(tasks);
                    client.Disconnect();
                }

                connection.Disconnect();

                return RedirectToAction("Index", "Home");
            }
        }

        async Task DownloadFileAsync(SftpClient client, string source, string destination)
        {
            using (var saveFile = System.IO.File.OpenWrite(destination))
            {
                var task = Task.Factory.FromAsync(client.BeginDownloadFile(source, saveFile), client.EndDownloadFile);
                await task;
            }
        }

        public async Task<Result> Fetch(string searchValue)
        {
            var api_key = Configuration.GetSection("TVDB").GetSection("api_key").Value;

            var fullSearch = Regex.Replace(searchValue, "[^0-9a-zA-Z ]+", "");

            var title = fullSearch.Substring(0, fullSearch.Length - 5);
            var year = fullSearch.Substring(fullSearch.Length - 4);

            using (var client = new HttpClient())
            {

                var response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=" + api_key + "&query=" + title);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<RootObject>(result);
                var movie = new Result();

                foreach (var res in json.results)
                {
                    var res_title = Regex.Replace(res.title, "[^0-9a-zA-Z ]+", "");

                    if (res_title.ToLower().Equals(title.ToLower()) && res.release_date.Substring(0,4).Equals(year))
                    {
                        movie = res;
                    }
                }

                //var single = json.results.SingleOrDefault(x => x.title.ToLower().Equals(title.ToLower()) && x.release_date.Substring(0, 4).Equals(year));

                return movie;
            }
        }

        public async Task<IActionResult> Index()
        {   
            return View();
        }
            
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> MovieList()
        {
            List<MovieModel> movies = await GetMovies();

            return View(movies);
        }
    }
}

