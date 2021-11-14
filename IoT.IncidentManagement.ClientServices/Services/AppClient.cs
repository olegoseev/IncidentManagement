using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientServices.Utils;

using Newtonsoft.Json;

using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public partial class AppClient : IAppClient
    {
        private readonly HttpClient httpClient;
        protected readonly Lazy<JsonSerializerSettings> settings;
        protected JsonSerializerSettings JsonSerializerSettings => settings.Value;
        public HttpClient HttpClient => httpClient;

        protected string URL { get; set; }

        public AppClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            settings = new Lazy<JsonSerializerSettings>(CreateSerializerSettings);
        }

        JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        #region Get

        public async Task<T> GetAsync<T>(CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(URL);

            var client = HttpClient;
            var disposeClient = false;
            try
            {
                using var request = new HttpRequestMessage();
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                PrepareRequest(client, request, urlBuilder);
                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                PrepareRequest(client, request, url);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                var disposeResponse = true;
                try
                {
                    var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                    {
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;
                    }

                    ProcessResponse(client, response);

                    var status = (int)response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse = await ReadObjectResponseAsync<T>(response, headers);
                        if (objectResponse.Object == null)
                        {
                            throw new ApiException("Response was null which was not expected.", status, objectResponse.Text, headers, null);
                        }
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync(CancellationToken.None);
                        throw new ApiException("The HTTP status code of the response was not expected (" + status + ").", status, responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
            finally
            {
                if (disposeClient)
                    client.Dispose();
            }
        }

        #endregion

        #region Add
        public virtual async Task<V> AddAsync<T, V>(T body, CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(URL);

            var client = httpClient;
            var disposeClient = false;
            try
            {
                using var request = new HttpRequestMessage();
                var content = new StringContent(JsonConvert.SerializeObject(body, settings.Value));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                PrepareRequest(client, request, urlBuilder);
                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                PrepareRequest(client, request, url);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                var disposeResponse = true;
                try
                {
                    var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                    {
                        foreach (var item_ in response.Content.Headers)
                            headers[item_.Key] = item_.Value;
                    }

                    ProcessResponse(client, response);

                    var status = (int)response.StatusCode;
                    if (status == 201 || status == 200)
                    {
                        var objectResponse = await ReadObjectResponseAsync<V>(response, headers);
                        if (objectResponse.Object == null)
                        {
                            throw new ApiException("Response was null which was not expected.", status, objectResponse.Text, headers, null);
                        }
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync(cancellationToken);
                        throw new ApiException("The HTTP status code of the response was not expected (" + status + ").", status, responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
            finally
            {
                if (disposeClient)
                    client.Dispose();
            }

        }

        public virtual async Task AddAsync<T>(T body, CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(URL);

            var client = httpClient;
            var disposeClient = false;
            try
            {
                using var request = new HttpRequestMessage();
                var content = new StringContent(JsonConvert.SerializeObject(body, settings.Value));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                PrepareRequest(client, request, urlBuilder);
                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                PrepareRequest(client, request, url);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                var disposeResponse = true;
                try
                {
                    var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                    {
                        foreach (var item_ in response.Content.Headers)
                            headers[item_.Key] = item_.Value;
                    }

                    ProcessResponse(client, response);

                    var status = (int)response.StatusCode;
                    if (status == 201 || status == 200)
                    {
                        return;
                    }
                    else
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync(cancellationToken);
                        throw new ApiException("The HTTP status code of the response was not expected (" + status + ").", status, responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
            finally
            {
                if (disposeClient)
                    client.Dispose();
            }

        }
        #endregion

        #region Update
        public virtual async Task UpdateAsync<T>(T body, CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(URL);

            var client = httpClient;
            var disposeClient = false;
            try
            {
                using var request = new HttpRequestMessage();
                var content = new StringContent(JsonConvert.SerializeObject(body, settings.Value));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("PUT");

                PrepareRequest(client, request, urlBuilder);
                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                PrepareRequest(client, request, url);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                var disposeResponse = true;
                try
                {
                    var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                    {
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;
                    }

                    ProcessResponse(client, response);

                    var status = (int)response.StatusCode;
                    if (status == 204)
                    {
                        return;
                    }
                    else
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync(CancellationToken.None);
                        throw new ApiException("The HTTP status code of the response was not expected (" + status + ").", status, responseData, headers, null);
                        //var objectResponse = await ReadObjectResponseAsync<ProblemDetails>(response, headers);
                        //if (objectResponse.Object == null)
                        //{
                        //    throw new ApiException("Response was null which was not expected.", status, objectResponse.Text, headers, null);
                        //}
                        //throw new ApiException<ProblemDetails>("Error", status, objectResponse.Text, headers, objectResponse.Object, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
            finally
            {
                if (disposeClient)
                    client.Dispose();
            }

        }
        #endregion

        #region Delete

        public async Task DeleteAsync(CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(URL);

            var client = httpClient;
            var disposeClient = false;
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = new HttpMethod("DELETE");

                    PrepareRequest(client, request, urlBuilder);
                    var url = urlBuilder.ToString();
                    request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client, request, url);

                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse = true;
                    try
                    {
                        var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                        if (response.Content != null && response.Content.Headers != null)
                        {
                            foreach (var item in response.Content.Headers)
                                headers[item.Key] = item.Value;
                        }

                        ProcessResponse(client, response);

                        var status = (int)response.StatusCode;
                        if (status == 204)
                        {
                            return;
                        }
                        else
                        {
                            var objectResponse = await ReadObjectResponseAsync<ProblemDetails>(response, headers);
                            if (objectResponse.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status, objectResponse.Text, headers, null);
                            }
                            throw new ApiException<ProblemDetails>("Error", status, objectResponse.Text, headers, objectResponse.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse)
                            response.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient)
                    client.Dispose();
            }
        }
        #endregion
    }
}
