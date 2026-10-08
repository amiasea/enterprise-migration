using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Amiasea.Enterprise.Data.Migration.Design;

public sealed class MigrationDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection services)
    {
        services.AddSingleton<
            IMigrationsCodeGenerator,
            QualifiedCSharpMigrationsGenerator>();
    }
}