// <summary>
// BlobalSuppressions.
// </summary>
// <copyright file="GlobalSuppressions.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Reviewed.", Scope = "member", Target = "~M:ExampleCode.Worker.OrganizeTaskServiceTest")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed.", Scope = "member", Target = "~M:ExampleCode.Program.Main(System.String[])")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed.", Scope = "member", Target = "~M:ExampleCode.Program.CreateHostBuilder(System.String[])~Microsoft.Extensions.Hosting.IHostBuilder")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Reviewed.", Scope = "member", Target = "~F:ExampleCode.Worker._logger")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Reviewed.", Scope = "member", Target = "~F:ExampleCode.Worker._bgwork")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Reviewed.", Scope = "member", Target = "~F:ExampleCode.Worker._task")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Reviewed.", Scope = "member", Target = "~M:ExampleCode.Worker.OrganizeBackgroundServiceTest")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Reviewed.", Scope = "member", Target = "~M:ExampleCode.Worker.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Reviewed.", Scope = "member", Target = "~P:ExampleCode.Worker.zzDebug")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.", Scope = "member", Target = "~P:ExampleCode.Worker.zzDebug")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Reviewed.", Scope = "member", Target = "~M:ExampleCode.Worker.Sleep")]
