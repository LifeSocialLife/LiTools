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
    using System.Reflection;
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
        // private readonly CancellationTokenSource tokenMain = new();

        private readonly Dictionary<string, BackgroundWorkModel> tasks = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkService"/> class.
        /// </summary>
        public BackgroundWorkService()
        {
            this.tasks = new Dictionary<string, BackgroundWorkModel>();

            this.zzDebug = "StoragePoolService";

            this._lockKey = new object();
        }

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
        /// Get all data from one running task.
        /// </summary>
        /// <param name="taskname">Name of task.</param>
        /// <returns>model BackgroundWorkModel - or null if task dont exist.</returns>
        public BackgroundWorkModel? GetTaskModel(string taskname)
        {
            if (!this.TaskExists(taskname))
            {
                return null;
            }

            // Get model and return.
            lock (this._lockKey)
            {
                return this.tasks[taskname];
            }
        }

        /// <summary>
        /// Get a list of all tasks in background work service.
        /// </summary>
        /// <returns>BackgroundWorkModel as list.</returns>
        public List<BackgroundWorkModel> GetAllTasksAsList()
        {
            var hej = new List<BackgroundWorkModel>();

            lock (this._lockKey)
            {
                hej = new List<BackgroundWorkModel>(this.tasks.Values);
            }

            this.zzDebug = "sdfdsf";
            return hej;
        }

        /// <summary>
        /// Cancel task.
        /// </summary>
        /// <param name="taskname">name of task to cancel.</param>
        public void Cancel(string taskname)
        {
            if (this.TaskExists(taskname))
            {
                lock (this._lockKey)
                {
                    this.tasks[taskname].Enabled = false;
                    this.tasks[taskname].BackgroundTaskShodbeRunning = false;
                    this.tasks[taskname].Token.Cancel();
                    this.zzDebug = "sdfds";
                }
            }
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

                this.tasks[data.Name].Taskwork = Task.Factory.StartNew(() => this.StartNewInsideWhileRunner(data), data.Token.Token, tmpdatatasktype, TaskScheduler.Default)
                    .ContinueWith(
                t =>
                {
                    this.TaskStatusRepporter(data.Name, t);
                });
            }

            return data.Name;
        }

        private async Task StartNewInsideWhileRunner(BackgroundWorkModel data)
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
                this.zzDebug = "sdfdsf";

                DateTime tmpStart = DateTime.UtcNow;
                data.TaskActionIsRunnig = true;

                data.TaskAction();

                data.TaskActionIsRunnig = false;
                data.TaskActionLastRunTime = DateTime.UtcNow - tmpStart;

                this.zzDebug = "sdfdsf";

                try
                {
                    await Task.Delay(data.WhileInterval, data.Token.Token);
                }
                catch (OperationCanceledException)
                {
                    continue;
                }

                // System.Threading.Thread.Sleep(data.WhileInterval);
                // Task.Delay(data.WhileInterval, data.Token.Token);

                // Task.WaitAny()
                // await Task.Delay(1000, stoppingToken);
                this.zzDebug = "sdfdsf";
            }

            if (Debugger.IsAttached)
            {
                Debugger.Break();
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
    public class BackgroundWorkModel // : BackgroundWorkPart1Model
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkModel"/> class.
        /// </summary>
        public BackgroundWorkModel()
        {
            this.Name = string.Empty;
            this.Enabled = true;
            this.Token = new CancellationTokenSource();

            this.TaskActionIsRunnig = false;
            this.TaskActionLastRunTime = default;

            this.DtLastCheckt = Convert.ToDateTime("2000-01-01 00:00:00");
            this.DtTaskCreated = DateTime.UtcNow;
            this.DtWhileLastRun = Convert.ToDateTime("2000-01-01 00:00:00");

            this.BackgroundTaskRunning = false;
            this.BackgroundTaskShodbeRunning = false;
            this.TaskType = TaskRunTypeEnum.None;
            this.AutoDeleteWhenDone = true;

            this.BackgroundTaskRunning = false;
            this.BackgroundTaskShodbeRunning = false;
            this.WhileInterval = 5000;
            this.WhileIntervalNoticInSek = 0;           // 0 min = not active.
            this.WhileIntervalWarningInSek = 0;   // 0 min = not active.
            this.WhileIntervalErrorInSek = 0;     // 0 min = not active.
        }

        /// <summary>
        /// Gets or sets name of this task.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is this work enabled or not?.
        /// </summary>
        public bool Enabled { get; set; }

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
        /// Gets or sets how long takes it for the work to run.
        /// </summary>
        public TimeSpan TaskActionLastRunTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is the action running right now. or is it in waiting mode.
        /// </summary>
        public bool TaskActionIsRunnig { get; set; }

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
        public int WhileInterval { get; set; }

        /// <summary>
        /// Gets or sets when shod a notic be trigger if DtWhileLastRun have not updated befor this time has passed.
        /// </summary>
        public ushort WhileIntervalNoticInSek { get; set; }

        /// <summary>
        /// Gets or sets when shod a Warning be trigger if DtWhileLastRun have not updated befor this time has passed.
        /// </summary>
        public ushort WhileIntervalWarningInSek { get; set; }

        /// <summary>
        /// Gets or sets when shod a Error be trigger if DtWhileLastRun have not updated befor this time has passed.
        /// </summary>
        public ushort WhileIntervalErrorInSek { get; set; }

        /// <summary>
        /// Gets or sets code that shod be run if notic is trigger.
        /// </summary>
        public Action? WhileIntervalNoticAction { get; set; }

        /// <summary>
        /// Gets or sets code that shod be run if Warning is trigger.
        /// </summary>
        public Action? WhileIntervalWarningAction { get; set; }

        /// <summary>
        /// Gets or sets code that shod be run if Error is trigger.
        /// </summary>
        public Action? WhileIntervalErrorAction { get; set; }
    }

    /*
    public class BackgroundWorkPart1Model
    {
        public BackgroundWorkPart1Model()
        {
            this.Name = string.Empty;
            this.Enabled = true;
        }
    }
    */
}
