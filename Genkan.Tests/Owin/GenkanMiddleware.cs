using Genkan.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Testing;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Owin;
using System.Threading.Tasks;
using System.Text;

namespace Genkan.Tests.Owin
{
    [TestClass]
    public class GenkanMiddleware
    {
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

            public Genkan.Owin.IOwinResponse GetResponse(IOwinRequest request)
            {
                return new TestOwinResponse();
            }
        }

        public class TestRequest : IRequest
        {

        }

        public class TestOwinResponse : Genkan.Owin.IOwinResponse
        {
            public Task WriteResponseAsync(Microsoft.Owin.IOwinResponse response)
            {
                var result = Encoding.UTF8.GetBytes("This is a first test.");
                response.Headers.Add("X-Genkan", new[] { "1" });
                return response.Body.WriteAsync(result, 0, result.Length);
            }
        }

        [TestMethod]
        public void TestGenkanIsActive()
        {
            using (var server = TestServer.Create(app =>
            {
                app.Use(typeof(Genkan.Owin.GenkanMiddleware), new TestGenkan(), new TestRequestResponseFactory());
            }))
            {
                HttpResponseMessage response = server.HttpClient.GetAsync("/?interface=Foo&method=bar").Result;
                Assert.IsTrue(response.Headers.Contains("X-Genkan"));
            }
        }
    }
}
