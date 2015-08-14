using System;

namespace Genkan
{
    public interface IApiDiscoverer
    {
        Func<IRequest, object> Resolve(string controllerName, string methodName);
    }
}
