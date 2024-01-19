using CircuitBreaker.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CircuitBreaker.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private string _baseAddres = "https://localhost:44361/Home/Ten";
        private AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy;
        private AsyncFallbackPolicy<HttpResponseMessage> _fallbackPolicy;

        public HomeController()
        {
            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                .RetryAsync(10, (d, c) =>
                {
                    string a = "Retry";
                    // we can log here
                });

            _circuitBreakerPolicy = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .Or<HttpRequestException>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10), (d, c) =>
                {
                    string a = "Break";
                }, () =>
                {
                    string a = "Reset";
                }, () =>
                {
                    string a = "Half";
                });

            _fallbackPolicy = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .Or<HttpRequestException>()
                .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent(typeof(Message), new Message
                    {
                        Id = 100,
                        Text = "متن پیش فرض"
                    }, new JsonMediaTypeFormatter())
                });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            //var result = await client.GetAsync(_baseAddres);

            var result = await _fallbackPolicy.ExecuteAsync(() => 
                                    _retryPolicy.ExecuteAsync(() => 
                                        _circuitBreakerPolicy.ExecuteAsync(() => 
                                            client.GetAsync(_baseAddres))));

            var str = await result.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ClientMessage>(str);
            return Ok(obj);
        }
    }
}