using System;
using System.Collections.Generic;

namespace Genkan
{
    class MethodInfo
    {
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public Func<IRequest, object> Delegate { get; set; }
        public List<ITobiraAttribute> Attributes { get; set; }

        public MethodInfo(string controllerName, string methodName, Func<IRequest, object> del, List<ITobiraAttribute> attributes)
        {
            
        }
    }

    public abstract class ITobiraAttribute : Attribute
    {
        public abstract bool Check(IRequest request, IResponse response);
    }
}
