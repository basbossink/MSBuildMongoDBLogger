properties { 
	$rootDir  = Resolve-Path .
	$buildOutputDir = Join-Path $rootDir build 
	$srcDir = Join-Path $rootDir src
	$toolsDir = Join-Path $rootDir tools
	$packagesDir = Join-Path $rootDir packages
	$nunitDir = Join-Path $packagesDir NUnit
	$nunitExe = Join-Path (Join-Path $nunitDir tools) nunit-console.exe
	$solutionFilePath = Join-Path $srcDir MSBuildMongoDBLogger.sln
	$assemblyInfoFilePath = Join-path $srcDir SharedAssemblyInfo.cs
	$config = "Debug"
	$platform = "Any CPU"
}

task default -depends Clean,Compile

task Clean {
	Remove-Item $buildOutputDir -Force -Recurse -ErrorAction SilentlyContinue
	exec { msbuild $solutionFilePath /p:Configuration=$config /p:Platform=$platform /t:Clean }
}

task UpdateVersionNumbers {
	$currentVersion = Get-SourceVersion $assemblyInfoFilePath
	
	$newVersion = New-Object -TypeName Version -ArgumentList $currentVersion.Major,`
		$currentVersion.Minor, ($currentVersion.Build+1), $currentVersion.revision
	Update-AssemblyInfo $assemblyInfoFilePath $config $newVersion
}

task Compile { 
	exec { msbuild $solutionFilePath /p:Configuration=$config /p:Platform=$platform }
}

task RunTests -depends Compile {
	$testsAssembly = Join-Path $buildOutputDir MSBuildMongoDBLoggerTests.dll
	
	exec { & $nunitExe /framework=4.0 /noshadow /nothread /domain=None $testsAssembly }
}

function Update-AssemblyInfo
{
	param (
		[string]$assemblyInfoFilePath,
		[string]$config,
		[Version]$version
		)
	$newVersion = 'AssemblyVersion("' + $version + '")'
	$newFileVersion = 'AssemblyFileVersion("' + $version + '")'
	$newConfiguration = 'AssemblyConfiguration("' + $config + '")'
	
	(Get-Content $assemblyInfoFilePath) | 
		ForEach-Object { $_ -replace 'AssemblyVersion\("[^"]+"\)', $newVersion } |
		ForEach-Object { $_ -replace 'AssemblyFileVersion\("[^"]+"\)', $newFileVersion } |
		ForEach-Object { $_ -replace 'AssemblyConfiguration\("[^"]+"\)', $newConfiguration } |
	Set-Content -Encoding UTF8 $assemblyInfoFilePath 
}

function Get-SourceVersion
{
	param
	(
		[string]$assemblyInfoFilePath
	)
	$pattern = '^\[assembly\: AssemblyVersion\("(?<major>\d+)\.(?<minor>\d+)\.(?<build>\d+)\.(?<revision>\d+)"\)'
	Select-String -Pattern $pattern -Path $assemblyInfoFilePath | Select-Object -expand Matches |
		ForEach-Object { New-Object -TypeName Version -ArgumentList $_.Groups['major'].Value, `
				$_.Groups['minor'].Value,`
				$_.Groups['build'].Value,`
				$_.Groups['revision'].Value
		}
}