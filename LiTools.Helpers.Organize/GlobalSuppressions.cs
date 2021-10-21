// <summary>
// GlobalSuppressions
// </summary>
// <copyright file="GlobalSuppressions.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Reviewed.", Scope = "member", Target = "~F:LiTools.Helpers.Organize.ParallelTask.Token")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Reviewed.", Scope = "member", Target = "~F:LiTools.Helpers.Organize.TaskService._lockKey")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Reviewed.", Scope = "type", Target = "~T:LiTools.Helpers.Organize.TaskServiceRunModel")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:Enumeration items should be documented", Justification = "Reviewed.", Scope = "type", Target = "~T:LiTools.Helpers.Organize.TaskRunTypeEnum")]
[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Reviewed.", Scope = "member", Target = "~M:LiTools.Helpers.Organize.TaskService.IsCancellationRequested(System.String)~System.Boolean")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Reviewed.", Scope = "member", Target = "~P:LiTools.Helpers.Organize.TaskService.BackgroundTaskRunning")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Reviewed.", Scope = "member", Target = "~F:LiTools.Helpers.Organize.BackgroundWorkService._lockKey")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Reviewed.", Scope = "type", Target = "~T:LiTools.Helpers.Organize.BackgroundWorkModel")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Reviewed.", Scope = "member", Target = "~P:LiTools.Helpers.Organize.TimeHelper.zzDebug")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Reviewed.", Scope = "member", Target = "~P:LiTools.Helpers.Organize.TimeHelper.zzDebug")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.", Scope = "member", Target = "~P:LiTools.Helpers.Organize.TimeHelper.zzDebug")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.", Scope = "member", Target = "~P:LiTools.Helpers.Organize.BackgroundWorkService.zzDebug")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.", Scope = "member", Target = "~P:LiTools.Helpers.Organize.BackgroundWorkService.zzDebug")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<Pending>", Scope = "member", Target = "~P:LiTools.Helpers.Organize.BackgroundWorkService.zzDebug")]
