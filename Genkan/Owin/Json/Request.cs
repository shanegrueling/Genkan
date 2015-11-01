using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Genkan.Owin.Json
{
    class Request : IRequest
    {
        private string _controllerName;
        private string _methodName;
        private JArray _parameter;

        public Request(string controllerName, string methodName, JArray parameter)
        {
            _controllerName = controllerName;
            _methodName = methodName;
            _parameter = parameter;
        }

        public string GetControllerName()
        {
            return _controllerName;
        }

        public string GetMethodName()
        {
            return _methodName;
        }

        public T GetParameter<T>(string name)
        {
            throw new NotImplementedException();
        }

        public T GetParameter<T>(int i)
        {
            return _parameter[i].ToObject<T>();
        }

        public object[] GetParameters()
        {
            throw new NotImplementedException();
        }

        public object[] GetParameters(ParameterInfo[] parameterInfo)
        {
            throw new NotImplementedException();
        }
    }
}
