// <summary>
// ExampleCode project starter.
// </summary>
// <copyright file="Program.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace ExampleCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Program starter.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<LiTools.Helpers.Organize.MenuHelperService>();

                    services.AddSingleton<LiTools.Helpers.Organize.BackgroundWorkService>();
                    services.AddSingleton<LiTools.Helpers.Organize.TaskService>();

                    // Encoding project.
                    services.AddSingleton<DemoTests.EncodingDemo>();
                    services.AddSingleton<LiTools.Helpers.Encoding.Cryptography.RsaCryptoService>();
                    services.AddHostedService<Worker>();
                });
    }
}
