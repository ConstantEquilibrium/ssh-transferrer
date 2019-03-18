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

        public async Task<IActionResult> GetMovies()
        {
            var remote = new { username = "robert", password = "<PASSWORD>", address = "<ADDRESS>" }; // UPDATE INFO

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
                        movies.Add(new MovieModel
                        {
                            File = item,
                            MovieData = await Fetch(item.Name)
                        });
                    }
                }
            }

            return null;
        }

        public async Task<IActionResult> Download()
        {
            var remote = new { username = "robert", password = "<PASSWORD>", address = "<ADDRESS>" }; // UPDATE INFO
            var local = new { username = "robert", password = "<PASSWORD>", address = "<ADDRESS>" };

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

                        var test = Fetch(item.Name);
                        //if (item.Name.Contains("S01E01"))
                        //{
                        //    tasks.Add(DownloadFileAsync(client, item.FullName, destination + "\\" + item.Name));
                        //}
                    }

                    //await Task.WhenAll(tasks);
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
            var fullSearch = Regex.Replace(searchValue, "[^0-9a-zA-Z ]+", "");

            var title = fullSearch.Substring(0, fullSearch.Length - 5);
            var year = fullSearch.Substring(fullSearch.Length - 4);

            using (var client = new HttpClient())
            {

                var response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=ff74666fe6c2adc1afc0cc931a96b258&query=" + title);
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
    }
}

