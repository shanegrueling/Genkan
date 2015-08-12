using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Testing;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Genkan.Tests.Owin
{
    [TestClass]
    public class GenkanMiddleware
    {
        class TestApiDiscoverer : IApiDiscoverer
        {
            public IEnumerable<MethodInfo> GetMethods(Type controller)
            {
                throw new NotImplementedException();
            }

            public bool IsController(Type controller)
            {
                throw new NotImplementedException();
            }

            IDictionary<string, Type> IApiDiscoverer.GetController()
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod]
        public void TestGenkanIsActive()
        {
            using (var server = TestServer.Create(app =>
            {
                app.Use(typeof(Genkan.Owin.GenkanMiddleware), new TestApiDiscoverer());
            }))
            {
                HttpResponseMessage response = server.HttpClient.GetAsync("/?interface=Foo&method=bar").Result;
                Assert.IsTrue(response.Headers.Contains("X-Genkan"));
            }
        }
        [TestMethod]
        public void TestGenkanIgnoresWrongCalls()
        {
            using (var server = TestServer.Create(app =>
            {
                app.Use(typeof(Genkan.Owin.GenkanMiddleware), new TestApiDiscoverer());
            }))
            {
                HttpResponseMessage response = server.HttpClient.GetAsync("/").Result;
                Assert.IsFalse(response.Headers.Contains("X-Genkan"));
            }
        }
    }
}
