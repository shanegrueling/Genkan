using Genkan.Owin;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Genkan.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:8080";
            WebApp.Start<Startup>(baseAddress);
            Console.WriteLine("Server Started; Press enter to Quit");

            HttpClient client = new HttpClient();

            StringContent queryString = new StringContent(@"['asdasdasd']");
            queryString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = client.PostAsync(baseAddress + "/?interface=Test&method=Hello", queryString).Result;

            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            Console.ReadLine();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(GenkanMiddleware), new Tobira(new ApiDiscoverer()), new Owin.Json.Factory());
        }
    }
}
