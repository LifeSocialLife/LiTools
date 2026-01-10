// <summary>
// LiSoLi - version service.
// </summary>
// <copyright file="VersionService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.VersionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Version handling service.
    /// </summary>
    public sealed class VersionService
    {
        /// <summary>
        /// Backing storage for per-software version entries.
        /// </summary>
        private readonly List<VersionModel> versionData = [];

        /// <summary>
        /// Gets synchronization root used for thread-safe access to <see cref="versionData"/>.
        /// </summary>
        private object VersionLock { get; } = new object();

        /// <summary>
        /// Return deep-copy list of all stored version entries.
        /// Thread-safe: locks <see cref="VersionLock"/> during enumeration and copy.
        /// </summary>
        /// <returns>Read-only list containing deep copies of all version entries..</returns>
        public IReadOnlyList<VersionModel> VersionGetAll()
        {
            lock (this.VersionLock)
            {
                return this.versionData.Select(v => v.Clone()).ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Return a deep copy of the version entry matching <paramref name="software"/>.
        /// </summary>
        /// <param name="software">The software identifier to lookup. Must not be />.
        /// Use <see cref="VersionGetAll"/> to retrieve all entries.</param>
        /// <returns>A deep copy of the matching <see cref="VersionModel"/>. If no entry exists a new default
        /// <see cref="VersionModel"/> with <see cref="VersionModel.SoftwareName"/> set to <paramref name="software"/>
        /// is returned.</returns>
        public VersionModel VersionGet(string software = "")
        {
            if (string.IsNullOrEmpty(software))
            {
                software = "litools.helpers.versionhandling";
            }

            lock (this.VersionLock)
            {
                var match = this.versionData.FirstOrDefault(v => v.SoftwareName.ToLower() == software.ToLower());
                if (match is null)
                {
                    // Return an empty/default model with the requested software name so callers always receive a usable object.
                    return new VersionModel { SoftwareName = software };
                }

                return match.Clone();
            }
        }

        /// <summary>
        /// Registers or updates a version entry by directly accepting a <see cref="VersionModel"/> instance.
        /// If an entry with the same <see cref="VersionModel.SoftwareName"/> already exists, it will be replaced.
        /// Otherwise, a new entry is added. Thread-safe.
        /// </summary>
        /// <param name="versionModel">The version model to register. Must not be null and must have a non-empty SoftwareName.</param>
        public void VersionRegister(VersionModel versionModel)
        {
            if (versionModel is null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(versionModel.SoftwareName))
            {
                return;
            }

            lock (this.VersionLock)
            {
                var softwareNameLower = versionModel.SoftwareName.ToLower();
                var idx = this.versionData.FindIndex(v => v.SoftwareName == softwareNameLower);

                // Clone the incoming model to ensure we store an independent copy
                var toStore = versionModel.Clone();
                toStore.SoftwareName = softwareNameLower;

                if (idx >= 0)
                {
                    // Replace existing entry
                    this.versionData[idx] = toStore;
                }
                else
                {
                    // Add new entry
                    this.versionData.Add(toStore);
                }
            }
        }

        /// <summary>
        /// Update the version model using a thread-safe mutate action.
        /// The mutate action must set <see cref="VersionModel.SoftwareName"/> to identify which item to update.
        /// If no existing item matches the mutated <see cref="VersionModel.SoftwareName"/>, a new entry is created using
        /// the mutated data.
        /// </summary>
        /// <param name="mutate">Action that mutates a deep copy of the current model.</param>
        public void VersionUpdate(Action<VersionModel> mutate)
        {
            if (mutate is null)
            {
                return;
            }

            lock (this.VersionLock)
            {
                // Start from a new default model so callers can set SoftwareName and other fields.
                var candidate = new VersionModel();

                // Allow the caller to initialize/modify the candidate.
                mutate(candidate);
                candidate.SoftwareName = candidate.SoftwareName.ToLower();

                // Use the SoftwareName set by the caller to find the target.
                var idx = this.versionData.FindIndex(v => v.SoftwareName == candidate.SoftwareName);
                var toStore = candidate.Clone();

                if (idx >= 0)
                {
                    // replace existing entry
                    this.versionData[idx] = toStore;
                }
                else
                {
                    // add new entry
                    this.versionData.Add(toStore);
                }
            }
        }

        /* dont remove yet. dont know if we need this code in future
        /// <summary>
        /// Sets the default version information atomically for the specified software entry.
        /// </summary>
        /// <param name="software">Software identifier to set.</param>
        /// <param name="bDate">The build date of the application instance.</param>
        /// <param name="bSite">The build site of the application instance.</param>
        /// <param name="bImage">The build image identifier of the application instance.</param>
        public void AppInstanceSet(SoftwareNameEnum software, string bDate, string bSite, string bImage)
        {
            lock (this.VersionLock)
            {
                if (software == SoftwareNameEnum.Default)
                {
                    for (int i = 0; i < this._versionData.Count; i++)
                    {
                        var current = this._versionData[i];
                        this._versionData[i] = new VersionModel
                        {
                            SoftwareName = current.SoftwareName,
                            VersionMajor = current.VersionMajor,
                            VersionMinor = current.VersionMinor,
                            VersionPatch = current.VersionPatch,
                            ReleaseType = current.ReleaseType,
                            VersionExtra = current.VersionExtra,
                            BuildDate = bDate,
                            BuildSite = bSite,
                            BuildImage = bImage,
                        };
                    }
                }
                else
                {
                    var idx = this._versionData.FindIndex(v => v.SoftwareName == software);
                    if (idx >= 0)
                    {
                        var current = this._versionData[idx];
                        this._versionData[idx] = new VersionModel
                        {
                            SoftwareName = current.SoftwareName,
                            VersionMajor = current.VersionMajor,
                            VersionMinor = current.VersionMinor,
                            VersionPatch = current.VersionPatch,
                            ReleaseType = current.ReleaseType,
                            VersionExtra = current.VersionExtra,
                            BuildDate = bDate,
                            BuildSite = bSite,
                            BuildImage = bImage,
                        };
                    }
                }
            }
        }
        */

        /*
        /// <summary>
        /// Alternative implementation using optimistic concurrency (compare-exchange).
        /// This approach avoids locks but may retry on contention.
        /// Currently commented out in favor of the lock-based implementation above.
        /// Retained for future consideration if lock contention becomes a performance bottleneck.
        /// </summary>

        /// <summary>
        /// Updates the version information using an optimistic compare-exchange loop to avoid lost updates.
        /// </summary>
        /// <param name="mutate">The mutation action applied to a deep copy of the current version model.</param>
        public void VersionUpdate(Action<VersionModel> mutate)
        {
            if (mutate is null)
            {
                return;
            }

            while (true)
            {
                var snapshot = Volatile.Read(ref this._version_model);
                var copy = snapshot.DeepCopy();
                mutate(copy);
                var original = Interlocked.CompareExchange(ref this._version_model, copy, snapshot);
                if (ReferenceEquals(original, snapshot))
                {
                    break; // success
                }

                // else: another thread updated concurrently; retry with the latest snapshot
            }
        }
        */
    }
}