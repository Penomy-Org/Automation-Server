# This is init script for the project (Window version)
# Execute this to init the neccessary tools for the project

# Set error handling
$ErrorActionPreference = "Stop"

# Format the project
Write-Output "Install necessary dotnet tools..."
dotnet tool restore
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet tool restore failed"
    exit $LASTEXITCODE
}
