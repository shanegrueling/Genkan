using Microsoft.Owin.Hosting;
using Owin;
using System;

namespace Genkan.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApp.Start<Startup>("http://localhost:8080");
            Console.WriteLine("Server Started; Press enter to Quit");
            Console.ReadLine();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(Owin.GenkanMiddleware));
        }
    }
}
