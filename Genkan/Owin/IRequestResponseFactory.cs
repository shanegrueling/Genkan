using Microsoft.Owin;

namespace Genkan.Owin
{
    public interface IRequestResponseFactory
    {
        IRequest GetRequest(IOwinRequest request);
        Genkan.Owin.IOwinResponse GetResponse(IOwinRequest request);
    }
}
