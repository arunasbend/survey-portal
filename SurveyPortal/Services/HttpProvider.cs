using System.Net.Http;
using System.Threading.Tasks;

namespace SurveyPortal.Services
{
    public interface IHttpProvider
    {
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
        //Task<HttpResponseMessage> GetAsync(string requestUri);
        //Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content);
        //Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
    public class HttpProvider: IHttpProvider
    {
        private readonly HttpClient _httpClient;

        public HttpProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return _httpClient.PostAsync(requestUri, content);
        }
    }
}
