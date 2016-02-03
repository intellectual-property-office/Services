function SetVersionFromConfigFile($filePath, [string]$newVersion)
{
	$file = New-Object XML
	$file.Load($filePath)
    $file.package.metadata.version = $newVersion
    $version = $file.package.metadata.version
	write-host "version in nuspec file is $version"
    $file.Save($filePath)
}
function CalulateVersionForNuSpec($filePath)
{
    $changesetNumber = $env:TFS_CHANGESET
    if ($changesetNumber -eq $null)
    {
        $changesetNumber = 0
    } 
	$file = New-Object XML
	$file.Load($filePath)
    $nuspecVersion = $file.package.metadata.version
	Write-Host "NuSpec version from metadata: $nuspecVersion"
	$nuspecVersion = [regex]::Replace($nuspecVersion, "(^\d+\.\d+\.)(\d+)$", '$1')
	$nuspecVersion = $nuspecVersion+[string]$changesetNumber
	Write-Host "NuSpec version to use: $nuspecVersion"
	
	return $nuspecVersion

	#$file.package.metadata.version = $nuspecVersion
	#$file.Save($filePath)
}
function CalculateVersion($SrcPath)
{
    $changesetNumber = $env:TFS_CHANGESET
    if ($changesetNumber -eq $null)
    {
        $changesetNumber = 0
    } 

    $PackageVersion = $PackageVersion.Replace("B", $changesetNumber)
     
    Write-Host "Transformed PackageVersion is $PackageVersion " 
    return $PackageVersion   
}


function RunNugetCommand($NugetPath, $file, $Arguments)
{
	$ps = new-object System.Diagnostics.Process
    $ps.StartInfo.Filename = "$NugetPath\nuget.exe"
    $ps.StartInfo.Arguments = $Arguments
    $ps.StartInfo.WorkingDirectory = $file.Directory.FullName
    $ps.StartInfo.RedirectStandardOutput = $True
    $ps.StartInfo.RedirectStandardError = $True
    $ps.StartInfo.UseShellExecute = $false
    $ps.start()
    if(!$ps.WaitForExit(30000)) 
    {
        $ps.Kill()
    }
    [string] $Out = $ps.StandardOutput.ReadToEnd();
    [string] $ErrOut = $ps.StandardError.ReadToEnd();
    Write-Host "Nuget pack Output of commandline" + $ps.StartInfo.Filename + " " + $ps.StartInfo.Arguments
    Write-Host $Out
    if ($ErrOut -ne "") 
    {
        Write-Error "Nuget pack Errors"
        Write-Error $ErrOut
    }
}

function CreatePackage($SrcPath, $PackageVersion, $NugetPath, $ProjectName)
{
	$NugetFolder = $SrcPath + "NugetPackages"
    if(!(Test-Path $NugetFolder))
    {
        New-Item -ItemType directory -Path $NugetFolder
    }

	$allFiles = Get-ChildItem $SrcPath
	$AllNuspecFiles
	foreach ($file in $allFiles)
    { 
		$fullName = $file.FullName
		if($file.Extension -eq ".nuspec")
		{
			$AllNuspecFiles += $file
		}
    }

    if($AllNuspecFiles.Length -gt 0)
    {
        write-host "building package from nuspec file"
        foreach ($file in $AllNuspecFiles)
        {   
            Write-Host "Modifying file " + $file.FullName

            #update version in nuspec file
            #Dont do this for now as we need to chekc out, update then check in again.
			#SetVersionFromConfigFile -filePath $file.FullName -newVersion $PackageVersion

			$PackageVersion = CalulateVersionForNuSpec -filePath $file.FullName


            #Create the .nupkg from the nuspec file
			RunNugetCommand -NugetPath $NugetPath -file $file -Arguments "pack `"$file`" -OutputDirectory `"$NugetFolder`" -Version ""$PackageVersion"""
        }
    }
    else
    {
        write-host "building package from project file"
		RunNugetCommand -NugetPath $NugetPath -file $file -Arguments "pack `"$SrcPath$ProjectName`" -IncludeReferencedProjects -OutputDirectory `"$NugetFolder`" -Version ""$PackageVersion"""
    }
}

function PublishPackages($SrcPath, $NugetPath, $NugetServer, $NugetServerPassword, $ProjectName)
{
	$NugetFolder = $SrcPath + "NugetPackages\"
	write-Host "publishing form $NugetFolder"
    $AllNugetPackageFiles = Get-ChildItem $NugetFolder*.* -include *.nupkg 
    foreach ($file in $AllNugetPackageFiles)
    { 
        write-host "PUBLISHING"

		$version = CalculateVersion($SrcPath) 
		$packageNameMinusIpo = $ProjectName.Substring(4)
		$packageName = $packageNameMinusIpo.Substring(0, $packageNameMinusIpo.Length-7)
		$artifactoryUrl = $NugetServer
		$nugetArguments = "push $file -Source $artifactoryUrl -apikey $NugetServerPassword"

		RunNugetCommand -NugetPath $NugetPath -file $file -Arguments $nugetArguments
    }
}

function RemoveExistingPackages($SrcPath)
{
    write-host "removing existing packages"
	$NugetFolder = $SrcPath + "NugetPackages\"
	if(test-path -path $NugetFolder) {
		$allFiles = @(Get-ChildItem $NugetFolder) #wrap results in an array otherwise we try and go thru the foreach even if there are no files, and get a null pointer exception
		foreach ($file in $allFiles)
		{ 
			write-host "File is $file"
			$fullName = $file.FullName
			write-Host "removing $fullName"
			Remove-Item $fullName
		}
	} else {
		write-host "Nothing to remove - $NugetFolder doesn't exist"
	}
}

function OnBuildServer()
{
    $changesetNumber = $env:TFS_CHANGESET
    if ($changesetNumber -eq $null)
    {
		write-Host "not on build server"
        return $false
    } 
    else
    {
		write-Host "on build server"
        return $true
    }
}

function Publish-NugetPackage
{
  Param
  (
    [string]$SrcPath,
    [string]$NugetPath,
    [string]$PackageVersion, 
    [string]$NugetServer,
    [string]$NugetServerPassword,
    [string]$ProjectName
  )
    $PackageVersion = CalculateVersion -$SrcPath
    RemoveExistingPackages -SrcPath $SrcPath
    CreatePackage -SrcPath $SrcPath -PackageVersion $PackageVersion -NugetPath $NugetPath -ProjectName $ProjectName
    if(OnBuildServer)
    {
        PublishPackages -SrcPath $SrcPath -NugetPath $NugetPath -NugetServer $NugetServer -NugetServerPassword $NugetServerPassword -ProjectName $ProjectName
    }
}