using System.Collections.Generic;

namespace Genkan.Example
{
    class ApiDiscoverer : IApiDiscoverer
    {
        private Dictionary<string, object> _controllers;

        public ApiDiscoverer()
        {
            _controllers = new Dictionary<string, object>
            {
                { "Test", new TestController() }
            };
        }

        public RPCInfo Resolve(string controllerName, string methodName)
        {
            var controller = _controllers[controllerName];

            var r = new RPCInfo(controller, controller.GetType().GetMethod(methodName));

            return r;
        }
    }
}
