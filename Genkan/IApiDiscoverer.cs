using System;
using System.Reflection;

namespace Genkan
{
    public interface IApiDiscoverer
    {
        RPCInfo Resolve(string controllerName, string methodName);
    }

    public class RPCInfo
    {
        public object Controller;
        public MethodInfo Method;

        public RPCInfo(object controller, MethodInfo method)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));
            if (method == null) throw new ArgumentNullException(nameof(method));

            Controller = controller;
            Method = method;
        }
    }
}
