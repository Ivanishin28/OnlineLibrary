using BookContext.DL.SqlServer;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace BookContext.Tests.Integration;

[SetUpFixture]
internal class TestContainerSetupFixture
{
    private static IContainer? _container;

    public static bool IsDockerAvailable => _container != null;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        try
        {
            _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
                .Build();
            await _container.StartAsync();

            var db = new BookDbContext(GetContextOptions(), null!);
            await db.Database.EnsureCreatedAsync();
        }
        catch (DockerUnavailableException)
        {
            Assert.Ignore("Docker is not available");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [OneTimeTearDown]
    public async Task GlobalTearDown()
    {
        if (_container != null)
        {
            await _container.DisposeAsync();
        }
    }

    public static DbContextOptions<BookDbContext> GetContextOptions()
    {
        if (!IsDockerAvailable)
        {
            throw new DockerUnavailableException("Can't provide context options");
        }

        return new DbContextOptionsBuilder<BookDbContext>()
            .UseSqlServer(_container!.GetConnectionString())
            .Options;
    }

    public static async Task Clear(BookDbContext db)
    {
        var tables = db
            .Model
            .GetEntityTypes()
            .Where(t => !t.IsOwned())
            .Where(t => t.GetTableName() != "__EFMigrationsHistory")
            .Select(t =>
            {
                var schema = t.GetSchema() ?? "public";
                var table = t.GetTableName();
                return $"\"{schema}\".\"{table}\"";
            })
            .Distinct()
            .ToList();

        if (!tables.Any())
        {
            return;
        }

        var truncateSql = $"TRUNCATE TABLE {string.Join(", ", tables)} RESTART IDENTITY CASCADE;";

        await db.Database.ExecuteSqlRawAsync(truncateSql);
    }
}
