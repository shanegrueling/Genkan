using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

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
                return i * 3;
            }

            public string TestMethodString(string s)
            {
                return s+" World!";
            }

            public string TestMethodTwoParameter(string s, string s2)
            {
                return s + " " + s2;
            }

            public int TestMethodIntArray(int[] i)
            {
                return i[0] * i[1];
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
                return (T)Parameters[i];
            }

            public object[] GetParameters(Type[] parameterInfo)
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

        [TestMethod()]
        public void TestCallString()
        {
            var apiDiscoverer = new TestApiDiscoverer();

            var tobira = new Tobira(apiDiscoverer);

            var request = new TestRequest("TestController", "TestMethodString", new object[] { "Hello" });
            var response = new TestResponse();

            tobira.Call(request, response);

            var info = apiDiscoverer.Resolve(request.GetControllerName(), request.GetMethodName());

            Assert.AreEqual(((TestController)info.Controller).TestMethodString("Hello"), response.Result);
        }

        [TestMethod()]
        public void TestCallTwoParameter()
        {
            var apiDiscoverer = new TestApiDiscoverer();

            var tobira = new Tobira(apiDiscoverer);

            var request = new TestRequest("TestController", "TestMethodTwoParameter", new object[] { "Hello", "Two Parameter" });
            var response = new TestResponse();

            tobira.Call(request, response);

            var info = apiDiscoverer.Resolve(request.GetControllerName(), request.GetMethodName());

            Assert.AreEqual(((TestController)info.Controller).TestMethodTwoParameter("Hello", "Two Parameter"), response.Result);
        }

        [TestMethod()]
        public void TestCallIntArray()
        {
            var apiDiscoverer = new TestApiDiscoverer();

            var tobira = new Tobira(apiDiscoverer);

            var request = new TestRequest("TestController", "TestMethodIntArray", new object[] { new[] { 2, 2 } });
            var response = new TestResponse();

            tobira.Call(request, response);

            var info = apiDiscoverer.Resolve(request.GetControllerName(), request.GetMethodName());

            Assert.AreEqual(((TestController)info.Controller).TestMethodIntArray(new[] { 2, 2 }), response.Result);
        }
    }
}