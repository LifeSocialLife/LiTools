// <summary>
// Worker.
// </summary>
// <copyright file="Worker.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace ExampleCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using LiTools.Helpers.Organize;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Worker class - background service.
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TaskService _task;
        private readonly BackgroundWorkService _bgwork;

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="taskService">TaskService.</param>
        /// <param name="backgroundWorkService">BackgroundWorkService.</param>
        public Worker(ILogger<Worker> logger, TaskService taskService, BackgroundWorkService backgroundWorkService)
        {
            this._logger = logger;
            this._task = taskService;
            this._bgwork = backgroundWorkService;
        }

        /// <summary>
        /// ConfigureServices.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Helper taskservice
            // services.AddSingleton<LiTools.Helpers.Organize.TaskService>();
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("Worker Starting at: {time}", DateTimeOffset.Now);

            this.OrganizeTaskServiceTest();
            this.OrganizeBackgroundServiceTest();

            while (!stoppingToken.IsCancellationRequested)
            {
                // this._logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }

            this._logger.LogInformation("Worker Ending at: {time}", DateTimeOffset.Now);
        }

        private void Sleep()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        #region Organize

        #region Background Work Service

        private void OrganizeBackgroundServiceTest()
        {
            this._logger.LogInformation("OrganizeBackgroundServiceTest - Starting");

            this._bgwork.Start(new BackgroundWorkModel()
            {
                AutoDeleteWhenDone = true,
                BackgroundTaskShodbeRunning = true,
                Name = "bgtest1",
                TaskAction = this.OrganizeBackgroundServiceTest_whilerun1,
                TaskType = TaskRunTypeEnum.Long,
                WhileInterval = 2000,
            });

            this._bgwork.Start(new BackgroundWorkModel()
            {
                AutoDeleteWhenDone = true,
                BackgroundTaskShodbeRunning = true,
                Name = "bgtest2",
                TaskAction = this.OrganizeBackgroundServiceTest_whilerun2,
                TaskType = TaskRunTypeEnum.Long,
                WhileInterval = 3000,
            });

            this._bgwork.Start(new BackgroundWorkModel()
            {
                AutoDeleteWhenDone = true,
                BackgroundTaskShodbeRunning = true,
                Name = "bgtest3",
                TaskAction = this.OrganizeBackgroundServiceTest_whilerun3,
                TaskType = TaskRunTypeEnum.Long,
                WhileInterval = 4000,
            });

            this._bgwork.Start(new BackgroundWorkModel()
            {
                AutoDeleteWhenDone = true,
                BackgroundTaskShodbeRunning = true,
                Name = "bgtest4",
                TaskAction = this.OrganizeBackgroundServiceTest_whilerun4,
                TaskType = TaskRunTypeEnum.Long,
                WhileInterval = 10000,
            });

            this._logger.LogInformation("OrganizeBackgroundServiceTest - Ending");

            // this.Sleep();
        }

        private void OrganizeBackgroundServiceTest_whilerun1()
        {
            this._logger.LogInformation("OrganizeBackgroundServiceTest_whilerun1 Running - interval 2 sek");
        }

        private void OrganizeBackgroundServiceTest_whilerun2()
        {
            this._logger.LogInformation("OrganizeBackgroundServiceTest_whilerun2 Running - interval 3 sek");
        }

        private void OrganizeBackgroundServiceTest_whilerun3()
        {
            this._logger.LogInformation("OrganizeBackgroundServiceTest_whilerun3 Running - interval 4 sek");
        }

        private void OrganizeBackgroundServiceTest_whilerun4()
        {
            this._logger.LogInformation("OrganizeBackgroundServiceTest_whilerun4 Running - interval 10 sek");
        }
        #endregion

        #region Organize Task Service

        private void OrganizeTaskServiceTest()
        {
            this._logger.LogInformation("OrganizeTaskServiceTests - Starting");

            this._task.StartNew(this.OrganizeTaskServiceTest_BgWork1, TaskRunTypeEnum.Long, "taskname1", true);
            this._task.StartNew(this.OrganizeTaskServiceTest_BgWork2, TaskRunTypeEnum.Long, "taskname2", true);

            this._logger.LogInformation("OrganizeTaskServiceTests - Ending");
        }

        private void OrganizeTaskServiceTest_BgWork1()
        {
            while (!this._task.IsCancellationRequested("taskname1"))
            {
                this._logger.LogInformation("OrganizeTaskServiceTest_BgWork1 Running - interval 3 sek");
                System.Threading.Thread.Sleep(3000);
            }
        }

        private void OrganizeTaskServiceTest_BgWork2()
        {
            while (!this._task.IsCancellationRequested("taskname2"))
            {
                this._logger.LogInformation("OrganizeTaskServiceTest_BgWork2 Running - interval 8 sek");
                System.Threading.Thread.Sleep(8000);
            }
        }

        #endregion

        #endregion
    }
}
