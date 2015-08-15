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

            var invoke = _apiDiscoverer.Resolve(controllerName, methodName);

            var result = invoke(request);

            response.SetResult(result);
        }
    }
}
