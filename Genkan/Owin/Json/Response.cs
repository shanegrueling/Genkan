using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Genkan.Owin.Json
{
    class Response : IOwinResponse
    {
        object _result;
        public void SetResult(object result)
        {
            _result = result;
        }

        public Task WriteResponseAsync(Microsoft.Owin.IOwinResponse response)
        {
            return response.WriteAsync(JsonConvert.SerializeObject(_result));
        }
    }
}
