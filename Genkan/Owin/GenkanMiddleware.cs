using Microsoft.Owin;
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

            return response.WriteResponseAsync(context.Response);
        }
    }
}
