using System;

namespace Genkan
{
    public class Tobira : ITobira
    {
        private IApiDiscoverer _apiDiscoverer;

        public Tobira(IApiDiscoverer apiDiscoverer)
        {
            if (apiDiscoverer == null) throw new ArgumentNullException(nameof(apiDiscoverer));

            _apiDiscoverer = apiDiscoverer;
        }

        public void Call(IRequest request, IResponse response)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (response == null) throw new ArgumentNullException(nameof(response));

            var controllerName = request.GetControllerName();
            var methodName = request.GetMethodName();

            var methodInfo = _apiDiscoverer.Resolve(controllerName, methodName);

            if(!PreCallCheck(methodInfo, request, response))
            {
                return;
            }

            var result = GetCallMethod(methodInfo)(request);
            response.SetResult(result);

            if (!PostCallCheck(methodInfo, request, response))
            {
                return;
            }
        }

        private bool PreCallCheck(RPCInfo method, IRequest request, IResponse response)
        {
            return true;
        }

        private bool PostCallCheck(RPCInfo method, IRequest request, IResponse response)
        {
            return true;
        }

        private Func<IRequest, object> GetCallMethod(RPCInfo info)
        {
            return req =>
            {
                return info.Method.Invoke(info.Controller, req.GetParameters());
            };

        }
    }
}
