// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using CompanyCalendar.Exporter.Google;
using CompanyCalendar.Exporter.Ics;
using CompanyCalendar.Hosting.WinForms;
using CompanyCalendar.Importer.Csv;
using CompanyCalendar.Importer.MsSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompanyCalendar.App.WinForms
{
    /// <summary>
    /// アプリケーションのエントリポイントを提供します。
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメインエントリポイントです。
        /// </summary>
        /// <param name="args">コマンドライン引数。</param>
        [STAThread]
        private static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            WinFormsApplication.Run<Form1>(Program.CreateHostBuilder(args));
        }

        /// <summary>
        /// <see cref="IHostBuilder" /> を生成します。
        /// </summary>
        /// <param name="args">コマンドライン引数。</param>
        /// <returns>生成された <see cref="IHostBuilder" />。</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                    services.AddConfiguredServices(hostingContext.Configuration, hostingContext.HostingEnvironment));

        /// <summary>
        /// 各サービスの依存関係を追加します。
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection" />。</param>
        /// <param name="configuration"><see cref="IConfiguration" />。</param>
        /// <param name="environment"><see cref="IHostEnvironment" />。</param>
        /// <returns>依存関係が追加された <see cref="IServiceCollection" />。</returns>
        private static IServiceCollection AddConfiguredServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment) =>
            services
                .AddGoogleExporter(configuration)
                .AddIcsExporter(configuration)
                .AddCsvImporter(configuration)
                .AddMsSqlImporter(configuration, environment)
                .AddTransient<Form1>();
    }
}
