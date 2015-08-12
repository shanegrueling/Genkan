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
            public IRequest Request = null;
            public IResponse Response;

            public void Call<T>(IRequest request, ref T response) where T : IResponse
            {
                Request = request;
                Response = response;
            }
        }

        public class TestRequestResponseFactory : IRequestResponseFactory
        {
            public IRequest Request = new TestRequest();
            public Genkan.Owin.IOwinResponse Response = new TestOwinResponse();

            public IRequest GetRequest(IOwinRequest request)
            {
                return Request;
            }

            public Genkan.Owin.IOwinResponse GetResponse(IOwinRequest request)
            {
                return Response;
            }
        }

        public class TestRequest : IRequest
        {

        }

        public class TestOwinResponse : Genkan.Owin.IOwinResponse
        {
            public Task WriteResponseAsync(Microsoft.Owin.IOwinResponse response)
            {
                return response.WriteAsync("");
            }
        }

        [TestMethod]
        public void TestInvoke()
        {
            var genkan = new TestGenkan();
            var testFactory = new TestRequestResponseFactory();
            using (var server = TestServer.Create(app =>
            {
                app.Use(typeof(Genkan.Owin.GenkanMiddleware), genkan, testFactory);
            }))
            {
                HttpResponseMessage response = server.HttpClient.GetAsync("/?interface=Foo&method=bar").Result;

                Assert.IsTrue(genkan.Request == testFactory.Request);
                Assert.IsTrue(genkan.Response == testFactory.Response);
            }
        }
    }
}
