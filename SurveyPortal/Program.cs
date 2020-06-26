using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace SurveyPortal
{
    public class Program
    {
        static IWebHost _host;
        static CancellationToken _ct;

        public static void SetCancelToken(CancellationToken ct) {
            _ct = ct;
        }

        public static void Main(string[] args)
        {
            _host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Path.Combine(Directory.GetCurrentDirectory()))
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            _host.Run(_ct);
        }

        public static IWebHost Host()
        {
            return _host;
        }
    }
}
