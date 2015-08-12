using System.Threading.Tasks;

namespace Genkan.Owin
{
    public interface IOwinResponse : IResponse
    {
        Task WriteResponseAsync(Microsoft.Owin.IOwinResponse response); 
    }
}
