// <summary>
// ParallelTask - Task helper.
// </summary>
// <copyright file="ParallelTask.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Organize
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Task helper to start and organize tasks.
    /// </summary>
    public static class ParallelTask
    {
        private static Dictionary<string, Task> tasks = new();

        /// <summary>
        /// Start Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="cancellationToken">Token.</param>
        /// <param name="taskname">Name of the task.</param>
        public static void Start(Func<Task> action, CancellationToken cancellationToken, string taskname = "")
        {
            LiTools.Helpers.Organize.ParallelTask.Start(action, cancellationToken, null, taskname);
        }

        /// <summary>
        /// Start Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="cancellationToken">Token.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="taskname">Name of the task.</param>
        public static void Start(Func<Task> action, CancellationToken cancellationToken, ILogger? logger, string taskname = "")
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (string.IsNullOrEmpty(taskname))
            {
                taskname = GenerateUniqueTaskname();
            }

            if (tasks.ContainsKey(taskname))
            {
                if (logger != null)
                {
                    logger.LogWarning("Task already exist!!");
                }

                return;
            }

            tasks.Add(
                taskname,
                Task.Factory.StartNew(
                action,
                cancellationToken,
                TaskCreationOptions.None,
                TaskScheduler.Default).ContinueWith(
                t =>
                {
                    if (logger != null)
                    {
                        logger.LogWarning(t.Exception, "Error while executing a parallel task.");
                    }
                },
                TaskContinuationOptions.OnlyOnFaulted));
        }

        /// <summary>
        /// Start longrunning Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="cancellationToken">Token.</param>
        /// <param name="taskname">Name of the task.</param>
        public static void StartLongRunning(Action action, CancellationToken cancellationToken, string taskname = "")
        {
            LiTools.Helpers.Organize.ParallelTask.StartLongRunning(action, cancellationToken, null, taskname);
        }

        /// <summary>
        /// Start longrunning Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="cancellationToken">Token.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="taskname">Name of the task.</param>
        public static void StartLongRunning(Action action, CancellationToken cancellationToken, ILogger? logger, string taskname = "")
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (string.IsNullOrEmpty(taskname))
            {
                taskname = GenerateUniqueTaskname();
            }

            if (tasks.ContainsKey(taskname))
            {
                if (logger != null)
                {
                    logger.LogWarning("Task already exist!!");
                }

                return;
            }

            tasks.Add(
                taskname,
                Task.Factory.StartNew(
                action,
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default).ContinueWith(
                t =>
                {
                    if (logger != null)
                    {
                        logger.LogWarning(t.Exception, "Error while executing a parallel task.");
                    }
                },
                TaskContinuationOptions.OnlyOnFaulted));
        }

        private static string GenerateUniqueTaskname()
        {
            string taskname;
            while (true)
            {
                taskname = LiTools.Helpers.Generate.Guid.AsString();
                if (tasks.ContainsKey(taskname))
                {
                    continue;
                }

                break;
            }

            return taskname;
        }
    }
}
