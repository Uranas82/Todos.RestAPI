using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI
{
    // - Sukurti aplikacij� "Todos app"
    // - TodoItems saugome Sql duomen� baz�j�

    // "TodoItem" model:
    // - Id
    // - Title
    // - Description
    // - Difficulty(Easy, Medium, Hard)
    // - IsDone
    // - DateCreated


    // RestAPI turi leisti:
    // - Gauti visus TodoItems
    // - Gauti konkret� TodoItem
    // - Prid�ti TodoItem
    // - Atnaujinti TodoItem
    // - I�trinti TodoItem

    // Kodas turi b�ti i�skirstytas per atitinkamus projektus

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
