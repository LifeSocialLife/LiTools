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
    /*
    // using Microsoft.Extensions.Logging;
    */

    /// <summary>
    /// Task helper to start and organize tasks.
    /// </summary>
    public static class ParallelTask
    {
        /// <summary>
        /// Token.
        /// </summary>
        public static CancellationTokenSource Token = new CancellationTokenSource();

        // private static Dictionary<string, Task> tasks = new();

        /// <summary>
        /// Start Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="cancellationToken">Token.</param>
        public static void Start(Func<Task> action, CancellationToken cancellationToken)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Task.Factory.StartNew(action, cancellationToken, TaskCreationOptions.None, TaskScheduler.Default).ContinueWith(
                t =>
                {
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }

        /// <summary>
        /// Start longrunning Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="cancellationToken">Token.</param>
        public static void StartLongRunning(Action action, CancellationToken cancellationToken)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Task.Factory.StartNew(
                action,
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default).ContinueWith(
                t =>
                {
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }

        /// <summary>
        /// Generate a unice Taskname.
        /// </summary>
        /// <returns>taskname as string.</returns>
        public static string GenerateUniqueTaskname()
        {
            return LiTools.Helpers.Generate.Guid.AsString();

            /*
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
            */
        }
    }
}
