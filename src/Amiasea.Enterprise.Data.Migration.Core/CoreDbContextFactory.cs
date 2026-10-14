using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Amiasea.Enterprise.Data.Core;

public class CoreDbContextFactory
    : IDesignTimeDbContextFactory<CoreDbContext>
{
    public CoreDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<CoreDbContext>()
            .UseSqlServer(options => {
                options.MigrationsAssembly(typeof(CoreDbContextFactory).Assembly.GetName().Name);

                options.MigrationsHistoryTable("__EFMigrationsHistory", "core");
            })
            .Options;

        return new CoreDbContext(options);
    }
}