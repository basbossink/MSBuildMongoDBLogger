<#
Copyright 2011 Bas Bossink <bas.bossink@gmail.com>
This file is part of MSBuildMongoDBLogger.

MSBuildMongoDBLogger is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MSBuildMongoDBLogger is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MSBuildMongoDBLogger.  If not, see <http://www.gnu.org/licenses/>.

#requires 2.0
#>
$toolsdir = Join-Path (Get-Location) tools
$nugetdir = Join-Path $toolsdir NuGet
$nuget = Join-Path $nugetdir NuGet.exe 

# run auto updating nuget bootstrapper
& $nuget
$env:PATH += ";$(nugetdir)"

& $nuget psake -o $toolsdir -x
& $nuget Chewie -o $toolsdir -x

$chewiedir = Join-Path $toolsdir chewie
Import-Module (Join-Path $chewiedir chewie.psm1)
Invoke-Chewie