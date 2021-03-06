﻿using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Ideas.Database
{
    class Program
    {
        static int Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            string connectionString = config.GetConnectionString("IdeasDb");

            //create the database if it doesn't exist
            EnsureDatabase.For.SqlDatabase(connectionString);

            //execute migration scripts
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScripts(new DirectoryScriptsProvider("Migrations"))
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