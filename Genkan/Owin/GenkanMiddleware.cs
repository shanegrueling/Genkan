using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace Genkan.Owin
{
    public class GenkanMiddleware : OwinMiddleware
    {
        private ITobira _tobira;
        private IRequestResponseFactory _requestResponseFactory;

        public GenkanMiddleware(OwinMiddleware next, ITobira tobira, IRequestResponseFactory requestResponseFactory)
        : base(next)
        {
            if (tobira == null) throw new ArgumentNullException(nameof(tobira));
            if (requestResponseFactory == null) throw new ArgumentNullException(nameof(requestResponseFactory));

            _requestResponseFactory = requestResponseFactory;
            _tobira = tobira;
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var response = _requestResponseFactory.GetResponse(context.Request);
            _tobira.Call(
                _requestResponseFactory.GetRequest(context.Request),
                response
                );

            return response.WriteResponseAsync(context.Response);
        }
    }
}
