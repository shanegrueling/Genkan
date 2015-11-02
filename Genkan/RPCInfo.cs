using System;
using System.Reflection;

namespace Genkan
{
    public class RPCInfo
    {
        public object Controller
        {
            get; private set;
        }
        public MethodInfo Method
        {
            get; private set;
        }

        public RPCInfo(object controller, MethodInfo method)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));
            if (method == null) throw new ArgumentNullException(nameof(method));

            Controller = controller;
            Method = method;
        }
    }
}
