using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Genkan.Owin.Json
{
    public class Factory : IRequestResponseFactory
    {
        public IRequest GetRequest(IOwinRequest request)
        {
            using (StreamReader sr = new StreamReader(request.Body))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    var iRequest = new Request(request.Query.Get("interface"), request.Query.Get("method"), JArray.Load(reader));

                    return iRequest;
                }
            }
        }

        public IOwinResponse GetResponse(IOwinRequest request)
        {
            return new Response();
        }
    }
}
