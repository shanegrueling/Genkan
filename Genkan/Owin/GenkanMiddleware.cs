using Microsoft.Owin;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Genkan.Owin
{
    public class GenkanMiddleware : OwinMiddleware
    {
        private IGenkan _genkan;
        private IRequestResponseFactory _requestResponseFactory;

        public GenkanMiddleware(OwinMiddleware next, IGenkan genkan, IRequestResponseFactory requestResponseFactory)
        : base(next)
        {
            _requestResponseFactory = requestResponseFactory;
            _genkan = genkan;
        }


        public override Task Invoke(IOwinContext context)
        { 
            var response = _requestResponseFactory.GetResponse(context.Request);
            _genkan.Call(
                _requestResponseFactory.GetRequest(context.Request),
                ref response
                );

            return response.WriteResponseAsync(context.Response);/*
            var pair = context.Request.Headers;
            if(pair.GetValues("interface") == null || pair.GetValues("method") == null)
            {
                return Next.Invoke(context);
            }


            var result = Encoding.UTF8.GetBytes("This is a first test.");
            context.Response.Headers.Add("X-Genkan", new[] { "1" });
            return context.Response.Body.WriteAsync(result, 0, result.Length);*/
        }
    }
}
