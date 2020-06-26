using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SurveyPortal;
using SurveyPortal.Controllers;

namespace UnitTests.Unit
{
    [TestFixture]
    public class StartupTests
    {
        private string _hostDirectory;
        Mock<IHostingEnvironment> _envMock;

        [SetUp]
        public void Setup()
        {
            _hostDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Path.Combine(_hostDirectory, "../../../../SurveyPortal"));    
     
            _envMock = new Mock<IHostingEnvironment>();
            _envMock.Setup(x => x.ContentRootPath).Returns(Directory.GetCurrentDirectory());
            _envMock.Setup(x => x.EnvironmentName).Returns("Development");
        }

        [TearDown]
        public void TearDown()
        {
            Directory.SetCurrentDirectory(_hostDirectory);
        }

        [Test]
        public void ConfigureServices_RegistersDependenciesCorrectly()
        {
            IServiceCollection services = new ServiceCollection();

            var target = new Startup(_envMock.Object);

            target.ConfigureServices(services);

            //  Mimic internal asp.net core logic.
            services.AddTransient<AnswerController>();
            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<AnswerController>();
            Assert.IsNotNull(controller);
        }

        [Test]
        public void Configure_Correctly()
        {
            _envMock.Setup(x => x.EnvironmentName).Returns("Development");

            IServiceCollection services = new ServiceCollection();
            var target = new Startup(_envMock.Object);
            target.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = new Mock<ILoggerFactory>();

            var app = new Mock<IApplicationBuilder>();
            app.Setup(x => x.ApplicationServices).Returns(serviceProvider);

            target.Configure(app.Object, _envMock.Object, loggerFactory.Object);
        }

        [Test]
        public void RunProgramSuccessfully()
        {
            try
            {
                var cancel = new CancellationTokenSource();
                var cancelThread = new CancellationTokenSource();

                var task = new Task(() =>
                {
                    var t = new Thread(() =>
                    {
                        Program.SetCancelToken(cancelThread.Token);
                        Program.Main(new string[0]);
                    });
                    t.Start();

                    Thread.Sleep(1000);

                    Assert.That(Program.Host(), Is.Not.Null);

                    cancelThread.Cancel();
                    t.Join();
                    cancel.Cancel();
                }, cancel.Token);

                task.Start();
                task.Wait(cancel.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Unrecognized exception");
            }
        }
    }
}