// <summary>
// ParallelTask - Task helper.
// </summary>
// <copyright file="ParallelTask.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Task helper to start diffrent tasks.
    /// </summary>
    public static class ParallelTask
    {
        private static Dictionary<string, Task> tasks = new();

        

        /// <summary>
        /// Start longrunning Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="cancellationToken">Token.</param>
        public static void StartLongRunning(Action action, ILogger logger, CancellationToken cancellationToken)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            Task.Factory.StartNew(
                action,
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default).ContinueWith(
                t =>
                {
                    logger.LogWarning(t.Exception, "Error while executing a parallel task.");
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
