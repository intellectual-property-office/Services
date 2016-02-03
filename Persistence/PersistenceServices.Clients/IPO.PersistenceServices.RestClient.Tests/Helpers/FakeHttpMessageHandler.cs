using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IPO.PersistenceServices.RestClient.Tests.Helpers
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public HttpRequestMessage Request { get; private set; }   

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage>SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(_response);
            return responseTask.Task;
        }
    }
}