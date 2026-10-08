FROM mcr.microsoft.com/dotnet/nightly/sdk:11.0

WORKDIR /app

COPY artifacts/bundles/ ./bundles/
COPY scripts/migrate.ps1 ./migrate.ps1

ENTRYPOINT ["pwsh", "-File", "/app/migrate.ps1"]