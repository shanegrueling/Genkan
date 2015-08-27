using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Genkan.Tests.Owin
{
    [TestClass()]
    public class TobiraTests
    {
        class TestApiDiscoverer : IApiDiscoverer
        {
            public RPCInfo Resolve(string controllerName, string methodName)
            {
                var testController = new TestController();

                var methodInfo = testController.GetType().GetMethod("TestMethod");
                var info = new RPCInfo(testController, methodInfo);
                return info;
            }
        }

        class TestController
        {
            public bool TestMethod()
            {
                return true;
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

            public object[] GetParameters()
            {
                return new object[0];
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

            var info = apiDiscoverer.Resolve(request.GetControllerName(), request.GetMethodName());

            Assert.AreEqual(((TestController)info.Controller).TestMethod(), response.Result);
        }
    }
}