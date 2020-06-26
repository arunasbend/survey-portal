using System.Threading.Tasks;

namespace SurveyPortal.Services
{
    public interface IEmailService 
    {
        Task<int> SendAsync(string to, string subject, string body);
    }
}
