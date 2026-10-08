using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Amiasea.Enterprise.Data.Migration.Design;

public sealed class QualifiedCSharpMigrationsGenerator(
    MigrationsCodeGeneratorDependencies dependencies,
    CSharpMigrationsGeneratorDependencies csharpDependencies)
    : CSharpMigrationsGenerator(dependencies, csharpDependencies)
{
    public override string GenerateMigration(
        string? migrationNamespace,
        string migrationName,
        IReadOnlyList<MigrationOperation> upOperations,
        IReadOnlyList<MigrationOperation> downOperations)
    {
        var code = base.GenerateMigration(
            migrationNamespace,
            migrationName,
            upOperations,
            downOperations);

        const string baseType = " : Migration";

        var index = code.IndexOf(baseType, StringComparison.Ordinal);

        if (index < 0)
        {
            throw new InvalidOperationException(
                "The generated migration did not contain the expected " +
                "' : Migration' base type.");
        }

        if (code.IndexOf(
                baseType,
                index + baseType.Length,
                StringComparison.Ordinal) >= 0)
        {
            throw new InvalidOperationException(
                "The generated migration contained multiple " +
                "' : Migration' base types.");
        }

        return code.Remove(index, baseType.Length)
            .Insert(
                index,
                " : Microsoft.EntityFrameworkCore.Migrations.Migration");
    }
}