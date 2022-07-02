using System;
using System.Data.Common;
using CompanyCalendar.Importer.MsSql;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit.Abstractions;

namespace CompanyCalendar.Tests.Importer.MsSql
{
    public abstract class AppDbContextUnitTestBase : XUnitTestBase, IDisposable
    {
        private readonly DbConnection _connection;
        private readonly PooledDbContextFactory<AppDbContext> _factory;

        protected AppDbContextUnitTestBase(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            // NOTE: https://docs.microsoft.com/ja-jp/ef/core/testing/testing-without-the-database#sqlite-in-memory
            // NOTE: https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Testing/TestingWithoutTheDatabase/SqliteInMemoryBloggingControllerTest.cs
            // NOTE: https://docs.microsoft.com/ja-jp/ef/core/performance/advanced-performance-topics?tabs=without-di%2Cwith-constant#dbcontext-pooling
            // NOTE: https://docs.microsoft.com/ja-jp/ef/core/logging-events-diagnostics/extensions-logging?tabs=v3
            // NOTE: https://stackoverflow.com/a/46172875/3363518
            this._connection = new SqliteConnection("Filename=:memory:");
            this._connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(this._connection)
                .UseLoggerFactory(this.XUnitLoggerFactory)
                .EnableSensitiveDataLogging()
                .Options;
            this._factory = new PooledDbContextFactory<AppDbContext>(options);

            using var context = this._factory.CreateDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            this.Cleanup();
        }

        public void Dispose() => this._connection.Dispose();

        protected AppDbContext CreateAppDbContext() => this._factory.CreateDbContext();

        protected void Cleanup()
        {
            using var context = this._factory.CreateDbContext();
            context.Database.BeginTransaction();
            
            context.CompanyHolidays.RemoveRange(context.CompanyHolidays);
            context.AddRange(
                new HolidayItem { Date = DateTime.Parse("2022/06/20"), Kind = HolidayKind.Shukkimbi, Summary = "HSC UnitTest0" },
                new HolidayItem { Date = DateTime.Parse("2022/06/20").AddDays(1), Kind = HolidayKind.Kyujitsu, Summary = "HSC UnitTest1" },
                new HolidayItem { Date = DateTime.Parse("2022/06/20").AddDays(2), Kind = HolidayKind.Shukujitsu, Summary = "HSC UnitTest2" },
                new HolidayItem { Date = DateTime.Parse("2022/06/20").AddDays(3), Kind = HolidayKind.YukyuKijumbi, Summary = "HSC UnitTest3" }
            );
            
            context.SaveChanges();
            context.Database.CommitTransaction();
        }
    }
}
