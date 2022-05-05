using System;
using Microsoft.AspNetCore.Mvc;

namespace Cbr_SystemGlobalServices.Managers
{
    public class HttpClientManager
    {
        public async Task<string > HttpGetAsync(HttpClient Client, Uri RequestUri)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            
                
                httpResponse = await Client.GetAsync(RequestUri);
                httpResponse.EnsureSuccessStatusCode();
                string httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

            
            return httpResponseBody;
        }
    }
}

