using Microsoft.Owin;
using System.Text;
using System.Threading.Tasks;

namespace Genkan.Owin
{
    public class GenkanMiddleware : OwinMiddleware
    {
        public GenkanMiddleware(OwinMiddleware next)
        : base(next)
        {
        }


        public override Task Invoke(IOwinContext context)
        {
            var result = Encoding.UTF8.GetBytes("This is a first test.");
            context.Response.Headers.Add("X-Genkan", new[] { "1" });
            return context.Response.Body.WriteAsync(result, 0, result.Length);
        }
    }
}
