using Newtonsoft.Json;
namespace SurveyPortal.DataContracts.Responses
{
    public class LoginResponse
    {
        [JsonProperty("firstname")]
        public string Firstname { get; set; }
        [JsonProperty("lastname")]
        public string Lastname { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }

        public string ErrorMessage { get; set; }
    }
}
 