using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SharpEcho.Recruiting.SpellChecker.Contracts;

namespace SharpEcho.Recruiting.SpellChecker.Core
{
    public class HttpHandler : IHttpClient
    {

        private HttpClient _client = new HttpClient();

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return _client.GetAsync(requestUri);
        }
    }
}
