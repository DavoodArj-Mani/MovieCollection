using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Model;

namespace MovieCollection.Services.App
{
    public static class UnitTestDetector
    {
        static UnitTestDetector()
        {
            string testAssemblyName = "xunit.runner.visualstudio.dotnetcore.testadapter";
            UnitTestDetector.IsInUnitTest = AppDomain.CurrentDomain.GetAssemblies()
                .Any(a => a.FullName.StartsWith(testAssemblyName));
        }

        public static bool IsInUnitTest { get; private set; }

        public static DbContextOptionsBuilder<ApplicationDbContext> builder()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
        }
    }
}
