// <summary>
// Task Service - Task handler service.
// </summary>
// <copyright file="TaskService.cs" company="LiSoLi">
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
    /// Service to handle all background work - Task running on software.
    /// </summary>
    public class TaskService
    {
        private readonly object _lockKey;

        // private readonly CancellationTokenSource tokenMain = new();

        /// <summary>
        /// Token.
        /// </summary>
        private readonly Dictionary<string, TaskServiceRunModel> tasks = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        public TaskService()
        {
            this.tasks = new Dictionary<string, TaskServiceRunModel>();

            this.zzDebug = "StoragePoolService";

            this._lockKey = new object();

            this.BackgroundTaskRunning = false;
            this.BackgroundTaskShodbeRunning = true;
            this.BackgroundTaskLastRun = Convert.ToDateTime("2000-01-01 00:00:00");

            this.BackgroundTaskChecker();
        }

        #region Background task Variables

        /// <summary>
        /// Gets or sets a value indicating whether is background task runinng?.
        /// </summary>
        public bool BackgroundTaskRunning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shod background task be running?.
        /// </summary>
        public bool BackgroundTaskShodbeRunning { get; set; }

        /// <summary>
        /// Gets or sets when was the background task last run?.
        /// </summary>
        public DateTime BackgroundTaskLastRun { get; set; }

        #endregion

        [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <summary>
        /// Start a new task to run.
        /// </summary>
        /// <param name="action">What shod be running?.</param>
        /// <param name="tasktype">Type of task this is.</param>
        /// <param name="taskname">Task name.</param>
        /// <param name="autoDeleteWhenDone">Delete task when it is done.</param>
        /// <exception cref="ArgumentNullException">error.</exception>
        /// <returns>Name of the work. if null. error starting background work.</returns>
        public string StartNew(Action action, TaskRunTypeEnum tasktype, string taskname = "", bool autoDeleteWhenDone = true)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            // is taskname null? create a new name.
            if (string.IsNullOrEmpty(taskname))
            {
                while (true)
                {
                    taskname = LiTools.Helpers.Organize.ParallelTask.GenerateUniqueTaskname();

                    if (this.TaskExists(taskname))
                    {
                        break;
                    }
                }
            }
            else
            {
                if (this.TaskExists(taskname))
                {
                    return string.Empty;
                }
            }

            lock (this._lockKey)
            {
                this.tasks.Add(taskname, new TaskServiceRunModel()
                {
                    TaskCreated = DateTime.UtcNow,
                    TaskName = taskname,
                    TaskType = tasktype,
                    AutoDeleteWhenDone = autoDeleteWhenDone,
                });

                TaskCreationOptions tmpdatatasktype = TaskCreationOptions.None;

                if (tasktype == TaskRunTypeEnum.Long)
                {
                    tmpdatatasktype = TaskCreationOptions.LongRunning;
                }

                this.tasks[taskname].Taskwork = Task.Factory.StartNew(action, this.tasks[taskname].TaskToken.Token, tmpdatatasktype, TaskScheduler.Default)
                    .ContinueWith(
                t =>
                {
                    this.TaskStatusRepporter(taskname, t);
                });
            }

            return taskname;
        }

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
        /// Get status of a task.
        /// </summary>
        /// <param name="taskname">name of task.</param>
        /// <returns>model TaskStatus.</returns>
        public TaskStatus GetStatus(string taskname)
        {
            if (this.TaskExists(taskname))
            {
                lock (this._lockKey)
                {
                    try
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        return this.tasks[taskname].Taskwork.Status;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                    catch
                    {
                        return TaskStatus.Faulted;
                    }
                }
            }

            return TaskStatus.Faulted;
        }

        /// <summary>
        /// Check a task. if the task is done. remove it from dictonary.
        /// </summary>
        /// <param name="taskname">name of task.</param>
        public void Check(string taskname)
        {
            bool tmpDeleteTask = false;

            if (this.TaskExists(taskname))
            {
                switch (this.GetStatus(taskname))
                {
                    case TaskStatus.RanToCompletion:
                        tmpDeleteTask = true;
                        break;

                    default:
                        if (Debugger.IsAttached)
                        {
                            Debugger.Break();
                        }

                        break;
                }
            }

            if (tmpDeleteTask)
            {
                lock (this._lockKey)
                {
                    if (this.tasks.ContainsKey(taskname))
                    {
                        this.tasks.Remove(taskname);
                    }
                }
            }
        }

        /// <summary>
        /// Cancel task.
        /// </summary>
        /// <param name="taskname">name of task to cancel.</param>
        public void CancelTask(string taskname)
        {
            if (this.TaskExists(taskname))
            {
                lock (this._lockKey)
                {
                    this.tasks[taskname].TaskToken.Cancel();
                }
            }
        }

        /// <summary>
        /// Shod this task be cancel?.
        /// </summary>
        /// <param name="taskname">name of task.</param>
        /// <returns>true or false.</returns>
        public bool IsCancellationRequested(string taskname)
        {
            if (!this.TaskExists(taskname))
            {
                return true;
            }

            try
            {
                return this.tasks[taskname].TaskToken.IsCancellationRequested;
            }

            // catch (Exception e)
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Background woker checker.
        /// </summary>
        public void BackgroundTaskChecker()
        {
            bool startBackgroundTask = false;

            if ((!this.BackgroundTaskRunning) && this.BackgroundTaskShodbeRunning)
            {
                // Background work is not running.. Start backgroundwork.
                startBackgroundTask = true;
            }
            else if ((DateTime.UtcNow - this.BackgroundTaskLastRun).TotalMinutes > 5)
            {
                // Background shod be running but have not reported anything for more then 5 min.
                // TODO Fix this.
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            }

            if (startBackgroundTask)
            {
                // Check if task already exist?
                if (this.TaskExists("main"))
                {
                    this.Check("main");
                    System.Threading.Thread.Sleep(500);
                }

                // if (LiTools.Helpers.Organize.ParallelTask.Exist("storagepoolservice"))
                if (this.TaskExists("main"))
                {
                    // Task already exist. what to do??
                    // TODO Fix this.
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }
                }
                else
                {
                    // Start task.
                    this.zzDebug = "dsf";
                    this.StartNew(this.BackgroundTask, TaskRunTypeEnum.Long, "main");

                    // LiTools.Helpers.Organize.ParallelTask.StartLongRunning(this.BackgroundTask, LiTools.Helpers.Organize.ParallelTask.Token.Token, "storagepoolservice");
                }
            }
        }

        private void BackgroundTask()
        {
            while (!this.IsCancellationRequested("main"))
            {
                this.BackgroundTaskRunning = true;
                this.BackgroundTaskLastRun = DateTime.UtcNow;

                if (!this.BackgroundTaskShodbeRunning)
                {
                    break;
                }

                try
                {
                    System.Threading.Thread.Sleep(1000);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

            this.zzDebug = "sdfdsf";

            this.BackgroundTaskRunning = false;
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
    /// Handler model for task works.
    /// </summary>
    public class TaskServiceRunModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskServiceRunModel"/> class.
        /// </summary>
        public TaskServiceRunModel()
        {
            this.TaskName = string.Empty;
            this.TaskToken = new CancellationTokenSource();
            this.TaskLastCheckt = Convert.ToDateTime("2000-01-01 00:00:00");
            this.TaskCreated = DateTime.UtcNow;
            this.TaskType = TaskRunTypeEnum.None;
            this.AutoDeleteWhenDone = true;
        }

        /// <summary>
        /// Gets or sets name of this task.
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets token to control this task.
        /// </summary>
        public CancellationTokenSource TaskToken { get; set; }

        /// <summary>
        /// Gets or sets task to be run.
        /// </summary>
        public Task? Taskwork { get; set; }

        /// <summary>
        /// Gets or sets when was this task last checket.
        /// </summary>
        public DateTime TaskLastCheckt { get; set; }

        /// <summary>
        /// Gets or sets when was this task created.
        /// </summary>
        public DateTime TaskCreated { get; set; }

        /// <summary>
        /// Gets or sets task is longrunning or shortrunning background work.
        /// </summary>
        public TaskRunTypeEnum TaskType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shod this task be deleted when done.
        /// </summary>
        public bool AutoDeleteWhenDone { get; set; }
    }
}
