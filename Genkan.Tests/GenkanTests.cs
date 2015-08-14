using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genkan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genkan.Tests.Owin
{
    [TestClass()]
    public class GenkanTests
    {
        class TestApiDiscoverer : IApiDiscoverer
        {
            public Func<IRequest, object> Resolve(string controllerName, string methodName)
            {
                return r => { return true; };
            }
        }

        class TestRequest : IRequest
        {
            public string GetControllerName()
            {
                return "Test";
            }

            public string GetMethodName()
            {
                return "Test";
            }

            public T GetParameter<T>(string name)
            {
                throw new NotImplementedException();
            }

            public T GetParameter<T>(int i)
            {
                throw new NotImplementedException();
            }
        }

        class TestResponse : IResponse
        {
            public object Result;

            public void SetResult(object result)
            {
                Result = result;
            }
        }
        [TestMethod(), ExpectedException(typeof(ArgumentNullException), "apiDiscoverer")]
        public void TestConstructorNull()
        {
            var tobira = new Tobira(null);
        }

        [TestMethod()]
        public void TestCall()
        {
            var apiDiscoverer = new TestApiDiscoverer();

            var tobira = new Tobira(apiDiscoverer);

            var request = new TestRequest();
            var response = new TestResponse();

            tobira.Call(request, response);

            Assert.AreEqual(apiDiscoverer.Resolve(request.GetControllerName(), request.GetMethodName())(request), response.Result);
        }
    }
}