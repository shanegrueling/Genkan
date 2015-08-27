using Genkan.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Testing;
using System.Net.Http;
using System;
using Microsoft.Owin;
using System.Threading.Tasks;

namespace Genkan.Tests.Owin
{
    [TestClass]
    public class GenkanMiddleware
    {
        class TestTobira : ITobira
        {
            public IRequest Request = null;
            public IResponse Response;

            public void Call(IRequest request, IResponse response)
            {
                Request = request;
                Response = response;
            }

            public void Call<T>(IRequest request, ref T response) where T : IResponse
            {
                
            }
        }

        class TestRequestResponseFactory : IRequestResponseFactory
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

        class TestRequest : IRequest
        {
            public string GetControllerName()
            {
                throw new NotImplementedException();
            }

            public string GetMethodName()
            {
                throw new NotImplementedException();
            }

            public T GetParameter<T>(string name)
            {
                throw new NotImplementedException();
            }

            public T GetParameter<T>(int i)
            {
                throw new NotImplementedException();
            }

            public object[] GetParameters()
            {
                throw new NotImplementedException();
            }
        }

        class TestOwinResponse : Genkan.Owin.IOwinResponse
        {
            public void SetResult(object result)
            {
                throw new NotImplementedException();
            }

            public Task WriteResponseAsync(Microsoft.Owin.IOwinResponse response)
            {
                return response.WriteAsync("");
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException), "tobira")]
        public void TestConstructorNull()
        {
            var genkanMiddleware = new Genkan.Owin.GenkanMiddleware(null, null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException), "requestResponseFactory")]
        public void TestConstructorIRequestResponseFactoryNull()
        {
            var tobira = new TestTobira();
            var genkanMiddleware = new Genkan.Owin.GenkanMiddleware(null, tobira, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException), "tobira")]
        public void TestConstructorITobiraNull()
        {
            var testFactory = new TestRequestResponseFactory();
            var genkanMiddleware = new Genkan.Owin.GenkanMiddleware(null, null, testFactory);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException), "context")]
        public void TestInvokeNull()
        {
            var tobira = new TestTobira();
            var testFactory = new TestRequestResponseFactory();
            var genkanMiddleware = new Genkan.Owin.GenkanMiddleware(null, tobira, testFactory);

            genkanMiddleware.Invoke(null);
        }

        [TestMethod]
        public void TestInvoke()
        {
            var tobira = new TestTobira();
            var testFactory = new TestRequestResponseFactory();
            using (var server = TestServer.Create(app =>
            {
                app.Use(typeof(Genkan.Owin.GenkanMiddleware), tobira, testFactory);
            }))
            {
                HttpResponseMessage response = server.HttpClient.GetAsync("/?interface=Foo&method=bar").Result;

                Assert.AreSame(tobira.Request, testFactory.Request);
                Assert.AreSame(tobira.Response, testFactory.Response);
            }
        }
    }
}
