// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompanyCalendar.Importer.MsSql
{
    /// <summary>
    /// <see cref="IServiceCollection" /> の拡張機能を提供します。
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// <see cref="ICsvLoader" /> に関するサービスの依存関係を追加します。
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection" />。</param>
        /// <param name="configuration"><see cref="IConfiguration" />。</param>
        /// <param name="environment"><see cref="IHostEnvironment" />。</param>
        /// <returns>依存関係が追加された <see cref="IServiceCollection" />。</returns>
        public static IServiceCollection AddMsSqlImporter(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);
            ArgumentNullException.ThrowIfNull(environment);

            return services
                .AddDbContext<AppDbContext>(
                    options =>
                    {
                        var dbLoaderOptions = configuration.GetSection(DbLoaderOptions.Key).Get<DbLoaderOptions>() ?? new DbLoaderOptions();

                        options.UseSqlServer(
                            dbLoaderOptions.ConnectionString,
                            sqlServerOptions => sqlServerOptions.CommandTimeout(dbLoaderOptions.CommandTimeoutSeconds));
                        options.EnableDetailedErrors();
                        if (environment.IsDevelopment())
                        {
                            options.EnableSensitiveDataLogging();
                        }
                    })
                .AddTransient<IDbLoader, DbLoader>();
        }
    }
}
