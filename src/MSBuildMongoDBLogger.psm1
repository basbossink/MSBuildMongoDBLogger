# Copyright 2011 Bas Bossink <bas.bossink@gmail.com>
# This file is part of MSBuildMongoDBLogger.
#
# MSBuildMongoDBLogger is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# MSBuildMongoDBLogger is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with MSBuildMongoDBLogger.  If not, see <http://www.gnu.org/licenses/>.
function Get-SolutionDir {
	Split-Path (get-item dte:\solution).FullName
}

function Get-Solution {
	Get-Item dte:\solution
}

function Get-IsSolutionOpen {
	(Get-Solution).IsOpen
}

$global:SolutionDir = Get-SolutionDir
$global:ProjectRoot = Split-Path $SolutionDir
$toolsDir = (Join-Path $ProjectRoot tools)
$nugetDir = Join-Path  $toolsDir NuGet
$env:PATH += ";$($nugetDir)"
cd $ProjectRoot
Import-Module (Join-Path $toolsDir psake\tools\psake.psm1)
Import-Module (Join-Path $toolsDir Chewie\chewie.psm1) 