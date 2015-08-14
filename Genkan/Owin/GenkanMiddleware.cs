using Microsoft.Owin;
using System;
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
            if (genkan == null) throw new ArgumentNullException(nameof(genkan));
            if (requestResponseFactory == null) throw new ArgumentNullException(nameof(requestResponseFactory));

            _requestResponseFactory = requestResponseFactory;
            _genkan = genkan;
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var response = _requestResponseFactory.GetResponse(context.Request);
            _genkan.Call(
                _requestResponseFactory.GetRequest(context.Request),
                response
                );

            return response.WriteResponseAsync(context.Response);
        }
    }
}
