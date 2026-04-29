<#
.SYNOPSIS
    Builds and packages a release ZIP of IP Address Changer.

.DESCRIPTION
    Runs preflight confirmation checks (version, CHANGELOG, README, git committed),
    then performs a clean + publish for a framework-dependent win-x64 build, and
    compresses the output into releases\IPAddressChanger-<version>.zip with a
    top-level folder so users get a clean extract.

    The version is read from the AssemblyVersion in IPAddressChanger.csproj.

.PARAMETER Configuration
    Build configuration. Defaults to Release.

.PARAMETER Runtime
    Runtime identifier. Defaults to win-x64.

.PARAMETER SkipChecks
    Skip the preflight confirmation checks. Useful for CI / scripted runs.

.EXAMPLE
    .\release.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Release',
    [string]$Runtime = 'win-x64',
    [switch]$SkipChecks
)

$ErrorActionPreference = 'Stop'

$projectFile   = Join-Path $PSScriptRoot 'IPAddressChanger.csproj'
$publishDir    = Join-Path $PSScriptRoot "bin\$Configuration\net8.0-windows\$Runtime\publish"
$releasesDir   = Join-Path $PSScriptRoot 'releases'
$changelogPath = Join-Path $PSScriptRoot 'CHANGELOG.md'

# Read version from the csproj so the zip filename and folder track AssemblyVersion automatically.
[xml]$proj = Get-Content -LiteralPath $projectFile
$version = ($proj.Project.PropertyGroup.AssemblyVersion | Where-Object { $_ } | Select-Object -First 1).Trim()
if ([string]::IsNullOrWhiteSpace($version)) {
    throw "Could not read <AssemblyVersion> from $projectFile."
}

function Confirm-Step {
    param([string]$Question)
    $answer = Read-Host "$Question (y/N)"
    return $answer -match '^[yY]'
}

function Abort-Release {
    param([string]$Reason)
    Write-Host ""
    Write-Host "Release aborted: $Reason" -ForegroundColor Red
    exit 1
}

# ----- Preflight -----
if (-not $SkipChecks) {
    Write-Host ""
    Write-Host "===== Pre-release checklist =====" -ForegroundColor Cyan
    Write-Host ""

    # 1) Version
    Write-Host "Version (from IPAddressChanger.csproj):" -NoNewline
    Write-Host " $version" -ForegroundColor Yellow
    if (-not (Confirm-Step "Is this version correct?")) { Abort-Release "version not confirmed" }

    # 2) CHANGELOG
    Write-Host ""
    Write-Host "CHANGELOG.md:"
    if (Test-Path $changelogPath) {
        $changelogContent = Get-Content -LiteralPath $changelogPath -Raw
        $escaped = [regex]::Escape($version)
        if ($changelogContent -match "## \[$escaped\]") {
            Write-Host "  Found entry for [$version]" -ForegroundColor Green
        } else {
            Write-Host "  No entry found for [$version]" -ForegroundColor Red
        }
    } else {
        Write-Host "  CHANGELOG.md not found" -ForegroundColor Red
    }
    if (-not (Confirm-Step "Is the CHANGELOG updated for this version?")) { Abort-Release "changelog not confirmed" }

    # 3) README
    Write-Host ""
    if (-not (Confirm-Step "Is the README up to date?")) { Abort-Release "README not confirmed" }

    # 4) Git
    Write-Host ""
    Write-Host "Git status:"
    $gitStatus = & git status --porcelain 2>&1
    $gitOk = ($LASTEXITCODE -eq 0)
    if (-not $gitOk) {
        Write-Host "  (not a git repo or git unavailable)" -ForegroundColor Red
    } elseif ([string]::IsNullOrWhiteSpace($gitStatus -join '')) {
        $gitBranch = (& git rev-parse --abbrev-ref HEAD).Trim()
        $gitHead = (& git log -1 --format="%h %s").Trim()
        Write-Host "  Clean working tree on '$gitBranch'" -ForegroundColor Green
        Write-Host "  HEAD: $gitHead" -ForegroundColor Green
    } else {
        Write-Host "  Uncommitted changes:" -ForegroundColor Red
        $gitStatus | ForEach-Object { Write-Host "    $_" -ForegroundColor Red }
    }
    if (-not (Confirm-Step "Is the current version committed?")) { Abort-Release "commit status not confirmed" }

    Write-Host ""
    Write-Host "Preflight passed. Starting build..." -ForegroundColor Cyan
    Write-Host ""
}

# ----- Build & package -----
Write-Host "Building IPAddressChanger v$version  ($Configuration / $Runtime)" -ForegroundColor Cyan

# Clean compiler artifacts so the publish below is a fresh build, not an incremental one.
& dotnet clean $projectFile -c $Configuration --verbosity minimal
if ($LASTEXITCODE -ne 0) {
    throw "dotnet clean failed (exit $LASTEXITCODE)."
}

# Ensure a clean publish output folder, otherwise stale files from a prior version can leak in.
if (Test-Path $publishDir) {
    Remove-Item -LiteralPath $publishDir -Recurse -Force
}

& dotnet publish $projectFile -c $Configuration -r $Runtime --self-contained false -p:PublishSingleFile=false
if ($LASTEXITCODE -ne 0) {
    throw "dotnet publish failed (exit $LASTEXITCODE)."
}

if (-not (Test-Path $releasesDir)) {
    New-Item -ItemType Directory -Path $releasesDir | Out-Null
}

# Stage into a versioned folder so the zip extracts to "IPAddressChanger-1.1.0.0\..." rather than
# scattering loose files into the user's Downloads folder.
$stageRoot = Join-Path $env:TEMP "IPAddressChanger-stage-$([Guid]::NewGuid().ToString('N'))"
$stagedAppDir = Join-Path $stageRoot "IPAddressChanger-$version"
New-Item -ItemType Directory -Path $stagedAppDir | Out-Null
try {
    Copy-Item -Path (Join-Path $publishDir '*') -Destination $stagedAppDir -Recurse -Force

    $zipPath = Join-Path $releasesDir "IPAddressChanger-$version.zip"
    if (Test-Path $zipPath) { Remove-Item -LiteralPath $zipPath -Force }
    Compress-Archive -Path (Join-Path $stageRoot '*') -DestinationPath $zipPath -CompressionLevel Optimal

    $sizeMB = '{0:N2}' -f ((Get-Item -LiteralPath $zipPath).Length / 1MB)
    Write-Host ""
    Write-Host "Created: $zipPath" -ForegroundColor Green
    Write-Host "Size:    $sizeMB MB" -ForegroundColor Green
}
finally {
    if (Test-Path $stageRoot) { Remove-Item -LiteralPath $stageRoot -Recurse -Force }
}
