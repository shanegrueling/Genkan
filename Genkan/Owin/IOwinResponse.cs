using System.Threading.Tasks;

namespace Genkan.Owin
{
    public interface IOwinResponse : IResponse
    {
        /// <summary>
        /// Writes the set result to <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The response to which the result should be send.</param>
        /// <returns></returns>
        Task WriteResponseAsync(Microsoft.Owin.IOwinResponse response); 
    }
}
