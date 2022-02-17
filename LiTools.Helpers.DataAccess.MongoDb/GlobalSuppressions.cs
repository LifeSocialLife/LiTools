// <summary>
// Global Suppressions.
// </summary>
// <copyright file="GlobalSuppressions.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "info", Scope = "member", Target = "~P:LiTools.Helpers.DataAccess.MongoDb.Services.MongoDbService.zzDebug")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "info.", Scope = "member", Target = "~P:LiTools.Helpers.DataAccess.MongoDb.Services.MongoDbService.zzDebug")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "info", Scope = "member", Target = "~P:LiTools.Helpers.DataAccess.MongoDb.Services.MongoDbService.ConnectionString")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "info", Scope = "type", Target = "~T:LiTools.Helpers.DataAccess.MongoDb.Services.MongoDbService")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "info.", Scope = "member", Target = "~P:LiTools.Helpers.DataAccess.MongoDb.Helpers.ServerHelper.zzDebug")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "info.", Scope = "member", Target = "~P:LiTools.Helpers.DataAccess.MongoDb.Helpers.ServerHelper.zzDebug")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "info.", Scope = "member", Target = "~F:LiTools.Helpers.DataAccess.MongoDb.Helpers.ServerHelper._connectionString")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "info.", Scope = "member", Target = "~F:LiTools.Helpers.DataAccess.MongoDb.Helpers.ServerHelper._connectionStringWrite")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "info.", Scope = "member", Target = "~F:LiTools.Helpers.DataAccess.MongoDb.Helpers.ServerHelper._databaseName")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "info.", Scope = "member", Target = "~M:LiTools.Helpers.DataAccess.MongoDb.Services.MongoDbService.GetModelSizeAsBsonAsBytes(System.Object)~System.Int32")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "info.", Scope = "member", Target = "~P:LiTools.Helpers.DataAccess.MongoDb.Helpers.ServerHelper.ConnectionString")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "info.", Scope = "member", Target = "~F:LiTools.Helpers.DataAccess.MongoDb.Helpers.ServerHelper._rebuldForceFull")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "info.", Scope = "member", Target = "~F:LiTools.Helpers.DataAccess.MongoDb.Helpers.ServerHelper._appnName")]
[assembly: SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "info.", Scope = "member", Target = "~M:LiTools.Helpers.DataAccess.MongoDb.Services.MongoDbService.ErrorHandlingWriteException(MongoDB.Driver.MongoWriteException)")]
[assembly: SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "info.", Scope = "member", Target = "~M:LiTools.Helpers.DataAccess.MongoDb.Services.MongoDbService.ErrorHandlingCommandException(MongoDB.Driver.MongoCommandException)")]
