using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Testing;
using System.Net.Http;

namespace Genkan.Tests.Owin
{
    [TestClass]
    public class GenkanMiddleware
    {
        [TestMethod]
        public void BasicTest()
        {
            using (var server = TestServer.Create(app =>
            {
                app.Use(typeof(Genkan.Owin.GenkanMiddleware));
            }))
            {
                HttpResponseMessage response = server.HttpClient.GetAsync("/").Result;
                Assert.IsTrue(response.Headers.Contains("X-Genkan"));
            }
        }
    }
}
