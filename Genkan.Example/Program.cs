using Genkan.Owin;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Owin;
using System.Threading.Tasks;
using System.Text;

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

            StringContent queryString = new StringContent(@"'['asdasdasd',{'a':123,'b':'Asdasd'}]'");
            queryString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = client.PostAsync(baseAddress + "/?interface=Foo&method=bar", queryString).Result;

            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            Console.ReadLine();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(Owin.GenkanMiddleware), new TestGenkan(), new TestRequestResponseFactory());
        }
    }

    public class TestGenkan : IGenkan
    {
        public void Call<T>(IRequest request, ref T response) where T : IResponse
        {
            
        }
    }

    public class TestRequestResponseFactory : IRequestResponseFactory
    {
        public IRequest GetRequest(IOwinRequest request)
        {
            return new TestRequest();
        }

        public Owin.IOwinResponse GetResponse(IOwinRequest request)
        {
            return new TestOwinResponse();
        }
    }

    public class TestRequest : IRequest
    {

    }

    public class TestOwinResponse : Owin.IOwinResponse
    {
        public Task WriteResponseAsync(Microsoft.Owin.IOwinResponse response)
        {
            var result = Encoding.UTF8.GetBytes("This is a first test.");
            response.Headers.Add("X-Genkan", new[] { "1" });
            return response.Body.WriteAsync(result, 0, result.Length);
        }
    }
}
