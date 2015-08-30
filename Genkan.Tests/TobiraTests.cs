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

                var methodInfo = testController.GetType().GetMethod(methodName);
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

            public int TestMethodInt(int i)
            {
                return i;
            }
        }

        class TestRequest : IRequest
        {
            public TestRequest(string controller, string method, object[] parameters)
            {
                Controller = controller;
                Method = method;
                Parameters = parameters;
            }

            public string Controller { get; private set; }
            public string Method { get; private set; }
            public object[] Parameters { get; private set; }

            public string GetControllerName()
            {
                return Controller;
            }

            public string GetMethodName()
            {
                return Method;
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
                return Parameters;
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

            var request = new TestRequest("TestController", "TestMethodInt", new object[] { 1 } );
            var response = new TestResponse();

            tobira.Call(request, response);

            var info = apiDiscoverer.Resolve(request.GetControllerName(), request.GetMethodName());

            Assert.AreEqual(((TestController)info.Controller).TestMethodInt(1), response.Result);
        }
    }
}