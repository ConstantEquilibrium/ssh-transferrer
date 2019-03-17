using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Renci.SshNet;
using SSH.NET_01.Models;

namespace SSH.NET_01.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Download()
        {
            var destination = "C:\\Users\\Robert\\Documents\\Visual Studio Projects\\SSH.NET\\SSH.NET-01";
            var source = "downloads/tvshows/Project Blue Book/Season 01";

            var connInfo = new ConnectionInfo("tiger.seedhost.eu", "robert", new PasswordAuthenticationMethod("robert", "pianomusik123"));
            using (var client = new SftpClient(connInfo))
            {
                client.Connect();
                var dir = client.ListDirectory(source);

                var tasks = new List<Task>();

                foreach (var item in dir)
                {
                    if (item.Name.Contains("S01E01"))
                    {
                        tasks.Add(DownloadFileAsync(client, item.FullName, destination + "\\" + item.Name));
                    }
                }

                await Task.WhenAll(tasks);
                client.Disconnect();
            }

            return RedirectToAction("Index", "Home");
        }

        async Task DownloadFileAsync(SftpClient client, string source, string destination)
        {
            using (var saveFile = System.IO.File.OpenWrite(destination))
            {
                var task = Task.Factory.FromAsync(client.BeginDownloadFile(source, saveFile), client.EndDownloadFile);
                await task;
            }
        }

        public async Task<IActionResult> fetch(string searchValue)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //client.BaseAddress = new Uri("https://api.themoviedb.org/3");
                    //client.DefaultRequestHeaders.Accept.Clear();
                    var response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=ff74666fe6c2adc1afc0cc931a96b258&query=" + "Avengers");
                    response.EnsureSuccessStatusCode();

                    //var result = await response.Content.ReadAsStringAsync();
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject(result);
                    return Ok(result);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting data from Database: {httpRequestException.Message}");
                }
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

