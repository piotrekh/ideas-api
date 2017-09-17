using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Ideas.TestData
{
    class Program
    {
        static int Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            string connectionString = config["connectionString"];

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScripts(new DirectoryScriptsProvider("TestData"))
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

#if DEBUG
            Console.ReadLine();
#endif

            return 0;
        }
    }
}