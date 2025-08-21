# This is publish script for the project (Window version)
# Run this file for publishing and check the out assets 
# In out folder (situated in SETUPPROJECT/out)

# Set error handling
$ErrorActionPreference = "Stop"

$RELEASE_CONFIGURATION_MODE_KEY = 'RELEASE_CONFIGURATION_MODE'
$RELEASE_CONFIGURATION_MODE_VALUE = ''

$PROJECT_NAME_KEY = 'PROJECT_NAME'
$PROJECT_NAME_VALUE = ''

# Define the path to the .env file
$parentPath = Split-Path -Path $PSScriptRoot -Parent
$envFilePath = "$parentPath\scripts\.env"

# Read the .env file and parse key-value pairs
if (Test-Path $envFilePath) {
    $envVars = @{}

    Get-Content $envFilePath | ForEach-Object {
        if ($_ -match "^\s*([^#][\w]+)\s*=\s*(.+)\s*$") {
            $envVars[$matches[1]] = $matches[2]
        }
    }

    # Check and assign values explicitly
    if ($envVars.ContainsKey($PROJECT_NAME_KEY)) {
        $PROJECT_NAME_VALUE = $envVars[$PROJECT_NAME_KEY]
    }
    if ($envVars.ContainsKey($RELEASE_CONFIGURATION_MODE_KEY)) {
        $RELEASE_CONFIGURATION_MODE_VALUE = $envVars[$RELEASE_CONFIGURATION_MODE_KEY]
    }
}

# Function to find the root directory containing the solution file
function Find-ProjectRoot {
    param (
        [string] $currentDir
    )

    while ($true) {
        # Try to find the sln in the current directory, if found, return something
        # otherwise, nothing return
        #
        # First 1 is from the first of the list, take 1 element
        $slnFile = Get-ChildItem -Path $currentDir -Filter *.sln -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($slnFile) {
            return $currentDir
        }

        # Try to back 1 level directory since, the sln is 
        # always in the root directory, so must go back
        # not going forward
        #
        # Then we assign new path to current dir
        $parentDir = Split-Path -Path $currentDir -Parent
        if ($parentDir -contains $PROJECT_NAME_VALUE) {
            return $null
        }
        $currentDir = $parentDir
    }

    return $null
}

# Determine the project root path containing the solution file
$projectRoot = Find-ProjectRoot -currentDir $(Split-Path -Path $MyInvocation.MyCommand.Path -Parent)
if (-not $projectRoot) {
    Write-Error "No solution file (.sln) found in the directory hierarchy."
    exit 1
}

# Set to working directory to project root
Write-Output "Project root path determined: $projectRoot"

Write-Output "Restore Project..."
dotnet restore $projectRoot
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet restore failed"
    exit $LASTEXITCODE
}

# Format the project
Write-Output "Publish project..."
dotnet publish --no-restore -c $RELEASE_CONFIGURATION_MODE_VALUE $projectRoot\src\Entry\
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet publish failed"
    exit $LASTEXITCODE
}
