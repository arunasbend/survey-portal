using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SurveyPortal.Data;

namespace UnitTests.Integration
{
    [SetUpFixture]
    public class BaseSetup
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
        }
    }

    public class Base
    {
        protected void UsingDatabase(Action<AppDbContext> action)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;

            using (var context = new AppDbContext(options))
            {
                action.Invoke(context);
                context.Dispose();
            }
        }
    }
}
