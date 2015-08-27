using Microsoft.Owin;

namespace Genkan.Owin
{
    public interface IRequestResponseFactory
    {
        IRequest GetRequest(IOwinRequest request);
        IOwinResponse GetResponse(IOwinRequest request);
    }
}
