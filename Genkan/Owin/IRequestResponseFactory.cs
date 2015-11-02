using Microsoft.Owin;

namespace Genkan.Owin
{
    public interface IRequestResponseFactory
    {
        /// <summary>
        /// Creates a <see cref="Request"/> from the given <paramref name="request"/>.
        /// </summary>
        /// <param name="request">The request from owin.</param>
        /// <returns>The request.</returns>
        IRequest GetRequest(IOwinRequest request);
        /// <summary>
        /// Creates a <see cref="Response"/>.
        /// </summary>
        /// <param name="request">The request from owin.</param>
        /// <returns>The Response object.</returns>
        IOwinResponse GetResponse(IOwinRequest request);
    }
}
