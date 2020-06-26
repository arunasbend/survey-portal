using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SurveyPortal.DataContracts.Dto;

namespace SurveyPortal.Services
{
    public class MailgunEmailService : IEmailService
    {
        public string ApiKey { get; set; }
        public string SandBox { get; set; }
        private IHttpProvider httpProvider;

        public MailgunEmailService(IOptions<MailgunSettings> setting, IHttpProvider httpProvider)
        {
            ApiKey = setting.Value.ApiKey;
            SandBox = setting.Value.Sandbox;
            this.httpProvider = httpProvider;
        }

        public Task<int> SendAsync(string to, string subject, string body)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://api.mailgun.net/v3/") };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetApiKey());

            var form = new Dictionary<string, string>
            {
                ["from"] = $"postmaster@{SandBox}",
                ["to"] = to,
                ["subject"] = subject,
                ["text"] = body
            };

            HttpResponseMessage response =
                httpProvider.PostAsync(SandBox + "/messages", new FormUrlEncodedContent(form)).Result;
            
            return Task.FromResult((int)response.StatusCode);
        }

        private string GetApiKey()
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{ApiKey}"));
        }
    }
}
