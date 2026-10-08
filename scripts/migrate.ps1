$connectionString = $env:SQL_CONNECTION_STRING

if ([string]::IsNullOrWhiteSpace($connectionString)) {
    throw "SQL_CONNECTION_STRING is required."
}

$bundles = @(
    Get-ChildItem -Path "/app/bundles" -File |
        Sort-Object Name
)

if ($bundles.Count -eq 0) {
    throw "No migration bundles found."
}

$processes = foreach ($bundle in $bundles) {
    Start-Process `
        -FilePath $bundle.FullName `
        -ArgumentList @("--connection", $connectionString) `
        -NoNewWindow `
        -PassThru
}

$failed = @()

foreach ($process in $processes) {
    $process.WaitForExit()

    if ($process.ExitCode -ne 0) {
        $failed += $process
    }

    $process.Dispose()
}

if ($failed.Count -gt 0) {
    throw "$($failed.Count) migration bundle(s) failed."
}