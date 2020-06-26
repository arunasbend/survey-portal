using Microsoft.Extensions.Options;
using NUnit.Framework;
using SurveyPortal.DataContracts.Dto;
using SurveyPortal.Services;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.Unit
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var content = request.Content.ReadAsStringAsync().Result;
            Assert.That(request.RequestUri.ToString(), Is.EqualTo("http://www.somewhere.whatever/messages"));
            Assert.That(request.Method.Method, Is.EqualTo("POST"));
            Assert.That(content.ToString(), Is.EqualTo("from=postmaster%40http%3A%2F%2Fwww.somewhere.whatever&to=to&subject=subject&text=body"));

            return Task.FromResult(new HttpResponseMessage { 
                StatusCode = System.Net.HttpStatusCode.OK,
            });
        }
    }

    [TestFixture]
    public class MailgunServiceTests
    {
        [Test]
        public async Task ShouldSendEmail()
        {
            IOptions<MailgunSettings> settings = Options.Create(new MailgunSettings
            {
                ApiKey = "Key",
                Sandbox = "http://www.somewhere.whatever",
            });
            var client = new HttpClient(new FakeHttpMessageHandler());
            var httpProvider = new HttpProvider(client);
            var mailgun = new MailgunEmailService(settings, httpProvider);
            
            var response = await mailgun.SendAsync("to", "subject", "body");

            Assert.That(response, Is.EqualTo(200));
        }
    }
}
