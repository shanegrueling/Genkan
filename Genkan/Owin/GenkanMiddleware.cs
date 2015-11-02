using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace Genkan.Owin
{
    public class GenkanMiddleware : OwinMiddleware
    {
        private ITobira _tobira;
        private IRequestResponseFactory _requestResponseFactory;
        /// <summary>
        /// Creates the GenkanMiddleware to use with owin.
        /// </summary>
        /// <param name="next">The next middleware to call after Genkan.</param>
        /// <param name="tobira">The <see cref="ITobira"/> to forward the call to.</param>
        /// <param name="requestResponseFactory">The <see cref="IRequestResponseFactory"/> to create the request and response.</param>
        public GenkanMiddleware(OwinMiddleware next, ITobira tobira, IRequestResponseFactory requestResponseFactory)
        : base(next)
        {
            if (tobira == null) throw new ArgumentNullException(nameof(tobira));
            if (requestResponseFactory == null) throw new ArgumentNullException(nameof(requestResponseFactory));

            _requestResponseFactory = requestResponseFactory;
            _tobira = tobira;
        }
        /// <summary>
        /// Invoke a request and send the response.
        /// </summary>
        /// <param name="context">The context on which the request should be processed.</param>
        /// <returns>The Task for writing the response.</returns>
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
