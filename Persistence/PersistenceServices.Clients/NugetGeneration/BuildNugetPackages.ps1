Param
(
[string] $srcDirectory,
[string] $PackageVersion = "1.0.B",
[string] $NugetServer = "http://repo1:8081/artifactory/api/nuget/nuget-release-local/IPO/",
[string] $NugetServerPassword = "nugetdeployer:AP7XLf9nUgMRBW8rbdFgvZA9Bp57BSFnrArmFy",
[string] $ProjectName
)
 
Write-Host "Running Pre Build Scripts"
$scriptRoot = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
Import-Module $scriptRoot\NugetBuildFunctions.psm1
write-host "source directory: $srcDirectory"
Publish-NugetPackage "$srcDirectory\" $scriptRoot $PackageVersion $NugetServer $NugetServerPassword $ProjectName