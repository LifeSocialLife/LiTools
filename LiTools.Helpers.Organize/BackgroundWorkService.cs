// <summary>
// Background task service.
// </summary>
// <copyright file="BackgroundWorkService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Organize
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Background work service.
    /// </summary>
    public class BackgroundWorkService
    {
        private readonly object _lockKey;

        /// <summary>
        /// Token.
        /// </summary>
        private CancellationTokenSource tokenMain = new CancellationTokenSource();

        private Dictionary<string, BackgroundWorkModel> tasks = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkService"/> class.
        /// </summary>
        public BackgroundWorkService()
        {
            this.tasks = new Dictionary<string, BackgroundWorkModel>();

            this.zzDebug = "StoragePoolService";

            this._lockKey = new object();
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <summary>
        /// Do the taskname exist?.
        /// </summary>
        /// <param name="taskname">Name of task.</param>
        /// <returns>true or false. if the taskname exist return is true.</returns>
        public bool TaskExists(string taskname)
        {
            lock (this._lockKey)
            {
                if (this.tasks.ContainsKey(taskname))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Start new background work.
        /// </summary>
        /// <param name="data">BackgroundWorkModel model.</param>
        /// <returns>name of the task. (data.name) as string.</returns>
        public string Start(BackgroundWorkModel data) // Action action, TaskRunTypeEnum tasktype, string taskname = "", bool autoDeleteWhenDone = true, ushort runInterval = 5000)
        {
            if (data.TaskAction == null)
            {
                throw new ArgumentNullException(nameof(data.TaskAction));
            }

            // is taskname null? create a new name.
            if (string.IsNullOrEmpty(data.Name))
            {
                while (true)
                {
                    data.Name = LiTools.Helpers.Organize.ParallelTask.GenerateUniqueTaskname();

                    if (this.TaskExists(data.Name))
                    {
                        break;
                    }
                }
            }
            else
            {
                if (this.TaskExists(data.Name))
                {
                    return string.Empty;
                }
            }

            lock (this._lockKey)
            {
                this.tasks.Add(data.Name, data);

                TaskCreationOptions tmpdatatasktype = TaskCreationOptions.None;

                if (data.TaskType == TaskRunTypeEnum.Long)
                {
                    tmpdatatasktype = TaskCreationOptions.LongRunning;
                }

                this.tasks[data.Name].Taskwork = Task.Factory.StartNew(() => this.StartNewInsideWhileRunner(data), this.tasks[data.Name].Token.Token, tmpdatatasktype, TaskScheduler.Default)
                    .ContinueWith(
                t =>
                {
                    this.TaskStatusRepporter(data.Name, t);
                });
            }

            return data.Name;
        }

        private void StartNewInsideWhileRunner(BackgroundWorkModel data)
        {
            if (data == null)
            {
                return;
            }

            if (data?.TaskAction == null)
            {
                return;
            }

            while (!data.Token.IsCancellationRequested)
            {
                data.DtWhileLastRun = DateTime.UtcNow;
                data.BackgroundTaskRunning = true;

                data.TaskAction();

                System.Threading.Thread.Sleep(data.WhileInterval);
            }

            this.zzDebug = "sdfdsf";

            data.BackgroundTaskRunning = false;
        }

        private void TaskStatusRepporter(string taskname, Task t)
        {
            bool tmpDeleteTask = false;

            if (t.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
            {
                if (this.TaskExists(taskname))
                {
                    lock (this._lockKey)
                    {
                        if (this.tasks[taskname].AutoDeleteWhenDone)
                        {
                            tmpDeleteTask = true;
                        }
                    }
                }

                this.zzDebug = "sdfdsf";
            }

            if (tmpDeleteTask)
            {
                lock (this._lockKey)
                {
                    this.tasks.Remove(taskname);
                }
            }

            this.zzDebug = "sdfdf";
        }
    }

    /// <summary>
    /// Allow own userdata to check and handling task. background work.
    /// </summary>
    public class BackgroundWorkModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkModel"/> class.
        /// </summary>
        public BackgroundWorkModel()
        {
            this.Name = string.Empty;
            this.Token = new CancellationTokenSource();

            this.DtLastCheckt = Convert.ToDateTime("2000-01-01 00:00:00");
            this.DtTaskCreated = Convert.ToDateTime("2000-01-01 00:00:00");
            this.DtWhileLastRun = Convert.ToDateTime("2000-01-01 00:00:00");

            this.BackgroundTaskRunning = false;
            this.BackgroundTaskShodbeRunning = false;
            this.TaskType = TaskRunTypeEnum.None;
            this.AutoDeleteWhenDone = true;

            this.BackgroundTaskRunning = false;
            this.BackgroundTaskShodbeRunning = false;
            this.WhileInterval = 5000;
        }

        /// <summary>
        /// Gets or sets name of this task.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets token to control this task.
        /// </summary>
        public CancellationTokenSource Token { get; set; }

        /// <summary>
        /// Gets or sets task to be run.
        /// </summary>
        public Task? Taskwork { get; set; }

        /// <summary>
        /// Gets or sets Action that this background work shod do inside while loop.
        /// </summary>
        public Action? TaskAction { get; set; }

        /// <summary>
        /// Gets or sets when was this task last checket.
        /// </summary>
        public DateTime DtLastCheckt { get; set; }

        /// <summary>
        /// Gets or sets when was this task created.
        /// </summary>
        public DateTime DtTaskCreated { get; set; }

        /// <summary>
        /// Gets or sets when was the background task last run?.
        /// </summary>
        public DateTime DtWhileLastRun { get; set; }

        /// <summary>
        /// Gets or sets task is longrunning or shortrunning background work.
        /// </summary>
        public TaskRunTypeEnum TaskType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shod this task be deleted when done.
        /// </summary>
        public bool AutoDeleteWhenDone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is background task runinng?.
        /// </summary>
        public bool BackgroundTaskRunning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shod background task be running?.
        /// </summary>
        public bool BackgroundTaskShodbeRunning { get; set; }

        /// <summary>
        /// Gets or sets when shod it be run. time in milleseconds.
        /// </summary>
        public ushort WhileInterval { get; set; }
    }
}
