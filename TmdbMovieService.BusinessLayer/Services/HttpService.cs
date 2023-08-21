using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovieService.BusinessLayer.Services.IServices;

namespace TmdbMovieService.BusinessLayer.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpService : IHttpService
    {
        public ILogger<HttpService> _logger { get; set; }

        public HttpService(ILogger<HttpService> logger)
        {
            _logger = logger;
        }
        public async Task<string> GetAsync(string url)
        {
            using HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(url);

            string stringResult = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                string content = JsonConvert.SerializeObject(response.Content);
                string reqcontent = JsonConvert.SerializeObject(response.RequestMessage.Content);
                string status = JsonConvert.SerializeObject(response.StatusCode);
                string uri = JsonConvert.SerializeObject(response.RequestMessage.RequestUri);

                _logger.LogError($"Request: {uri} - {reqcontent} \n\nResponse {status} - {content}", LogLevel.Error);


            }

            return stringResult;
        }

        public async Task<string> PostAsync(string url, object requestBody)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));

            client.DefaultRequestHeaders.Connection.Add("keep-alive");

            var data = JsonConvert.SerializeObject(requestBody, Formatting.Indented);

            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            var stringResult = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                string @status = JsonConvert.SerializeObject(response.StatusCode);
                string @uri = JsonConvert.SerializeObject(response.RequestMessage.RequestUri);

                _logger.LogError($"Request: {@uri} \n\nResponse {@status} - {stringResult}", LogLevel.Error);
            }


            return stringResult;
        }
    }
}
