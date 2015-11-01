using System;
using System.Collections.Generic;
using System.Reflection.Emit;

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

        /*private Func<IRequest, object> GetCallMethod(RPCInfo info)
        {
            return req =>
            {
                return info.Method.Invoke(info.Controller, req.GetParameters());
            };

        }*/

        private Func<IRequest, object> GetCallMethod(RPCInfo info)
        {
            // Create a dynamic method to write our own function           
            DynamicMethod call = new DynamicMethod(
                "",
                typeof(object),
                new Type[] { info.Method.DeclaringType, typeof(IRequest) },
                info.Method.DeclaringType
            );

            ILGenerator generator = call.GetILGenerator();

            //We need to get all parameter out of the IRequest
            //and cast/unbox them to the right type.
            List<LocalBuilder> lbList = new List<LocalBuilder>();
            foreach (var parameter in info.Method.GetParameters())
            {
                LocalBuilder lb = generator.DeclareLocal(parameter.ParameterType);
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Ldc_I4, parameter.Position);

                var getParameterMethod = typeof(IRequest).GetMethod("GetParameter", new[] { typeof(int) }).MakeGenericMethod(new Type[] { parameter.ParameterType });
                generator.Emit(OpCodes.Callvirt, getParameterMethod);

                generator.Emit(OpCodes.Stloc, lb);
                lbList.Add(lb);
            }
            //Now call the method
            generator.Emit(OpCodes.Ldarg_0);
            foreach (LocalBuilder lb in lbList)
            {
                generator.Emit(OpCodes.Ldloc, lb);
            }
            generator.Emit(OpCodes.Callvirt, info.Method);

            //Return the result
            if (!info.Method.ReturnType.IsClass)
            {
                //built-ins must be boxed to become an object
                generator.Emit(OpCodes.Box, info.Method.ReturnType);
            }
            generator.Emit(OpCodes.Ret);


            return call.CreateDelegate(
                    typeof(Func<IRequest, object>),
                    info.Controller
                ) as Func<IRequest, object>;
        }
    }
}
