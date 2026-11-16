using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aimsby.Infrastructure.Persistence;

/// <summary>
/// Used only by the EF Core CLI (`dotnet ef migrations add`, etc.) at design time.
/// The connection string is a placeholder — generating a migration inspects the
/// model, it does not connect to the database. Runtime wiring lives in the API host.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Legacy id, kept on purpose: the database file keeps its name from before the rename.
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=data/personaos.db")
            .Options;

        return new AppDbContext(options);
    }
}
