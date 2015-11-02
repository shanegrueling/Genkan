using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Genkan.Owin.Json
{
    class Request : IRequest
    {
        private string _controllerName;
        private string _methodName;
        private JArray _parameter;

        /// <summary>
        /// Creates a Request object and prepares the internal state.
        /// </summary>
        /// <param name="controllerName">The name of the controller to call</param>
        /// <param name="methodName">The name of the method to call.</param>
        /// <param name="parameter">The JArray containg the parameter given from the client.</param>
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
            return _parameter[name].ToObject<T>();
        }

        public T GetParameter<T>(int i)
        {
            return _parameter[i].ToObject<T>();
        }

        public object[] GetParameters(Type[] types)
        {
            var o = new List<object>();
            var i = 0;
            foreach(var t in types)
            {
                o.Add(_parameter[i].ToObject(t));
                ++i;
            }

            return o.ToArray();
        }
    }
}
