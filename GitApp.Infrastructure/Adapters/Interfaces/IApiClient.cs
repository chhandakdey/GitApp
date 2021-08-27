﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Infrastructure.Adapters.Interfaces
{
    public interface IApiClient
    {
        public Task<HttpResponseMessage> SendAsync(string targetUrl, HttpMethod httpMethod,
            IEnumerable<KeyValuePair<string, string>> headers = null, HttpContent httpContent = null);
    }
}
