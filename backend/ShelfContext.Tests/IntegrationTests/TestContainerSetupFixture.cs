using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShelfContext.DL.SqlServer;
using Testcontainers.MsSql;

namespace ShelfContext.Tests.IntegrationTests;

[SetUpFixture]
internal class TestContainerSetupFixture
{
    private static IContainer? _container;
    private static ICollection<IEntityType>? _ordered;

    public static bool IsDockerAvailable => _container != null;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        try
        {
            _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
                .Build();
            await _container.StartAsync();

            var db = new ShelfDbContext(GetContextOptions());
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

    public static DbContextOptions<ShelfDbContext> GetContextOptions()
    {
        if (!IsDockerAvailable)
        {
            throw new DockerUnavailableException("Can't provide context options");
        }

        return new DbContextOptionsBuilder<ShelfDbContext>()
            .UseSqlServer(_container!.GetConnectionString())
            .Options;
    }

    public static async Task Clear(ShelfDbContext db)
    {
        _ordered ??= GetOrderedEntities(db);

        foreach (var t in _ordered)
        {
            var schema = t.GetSchema() ?? "dbo";
            var table = t.GetTableName()!;
            var sql = "DELETE FROM [" + schema + "].[" + table + "]";
            await db.Database.ExecuteSqlRawAsync(sql);
        }
    }

    private static ICollection<IEntityType> GetOrderedEntities(ShelfDbContext db)
    {
        var entityTypes = db.Model
            .GetEntityTypes()
            .Where(t => !t.IsOwned())
            .Where(t =>
            {
                var name = t.GetTableName();
                return name != null && name != "__EFMigrationsHistory";
            })
            .ToList();

        if (!entityTypes.Any())
        {
            return new List<IEntityType>();
        }

        var typeSet = entityTypes.ToHashSet();
        var dependentsRemaining = entityTypes.ToDictionary(t => t, _ => 0);
        foreach (var t in entityTypes)
        {
            foreach (var fk in t.GetForeignKeys())
            {
                var principal = fk.PrincipalEntityType;
                if (typeSet.Contains(principal))
                {
                    dependentsRemaining[principal]++;
                }
            }
        }

        var queue = new Queue<IEntityType>(entityTypes.Where(t => dependentsRemaining[t] == 0));
        var ordered = new List<IEntityType>();
        while (queue.Count > 0)
        {
            var t = queue.Dequeue();
            ordered.Add(t);
            foreach (var fk in t.GetForeignKeys())
            {
                var principal = fk.PrincipalEntityType;
                if (!typeSet.Contains(principal))
                {
                    continue;
                }

                if (--dependentsRemaining[principal] == 0)
                {
                    queue.Enqueue(principal);
                }
            }
        }

        if (ordered.Count != entityTypes.Count)
        {
            throw new InvalidOperationException("Could not order tables for delete (possible circular FKs).");
        }

        return ordered;
    }
}
