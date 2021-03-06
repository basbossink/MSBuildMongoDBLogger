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
@{

# Script module or binary module file associated with this manifest
ModuleToProcess = 'MSBuildMongoDBLogger.psm1'

# Version number of this module.
ModuleVersion = '0.0.0.1'

# ID used to uniquely identify this module
GUID = '{0bd506cd-1b2b-408d-8f1f-7f821cb0fdbf}'

# Author of this module
Author = 'Bas Bossink'

# Company or vendor of this module
CompanyName = ''

# Copyright statement for this module
Copyright = '© 2011 Bas Bossink <bas.bossink@gmail.com>. All rights reserved.'

# Description of the functionality provided by this module
Description = 'A PowerShell module providing cmdlets for use in StudioShell'

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '2.0'

# Minimum version of the .NET Framework required by this module
DotNetFrameworkVersion = '2.0'

# Minimum version of the common language runtime (CLR) required by this module
CLRVersion = '2.0.50727'

# Processor architecture (None, X86, Amd64, IA64) required by this module
ProcessorArchitecture = 'None'

# Modules that must be imported into the global environment prior to importing
# this module
RequiredModules = @()

# Assemblies that must be loaded prior to importing this module
RequiredAssemblies = @()

# Script files (.ps1) that are run in the caller's environment prior to
# importing this module
ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
FormatsToProcess = @()

# Modules to import as nested modules of the module specified in
# ModuleToProcess
NestedModules = @()

# Functions to export from this module
FunctionsToExport = '*'

# Cmdlets to export from this module
CmdletsToExport = '*'

# Variables to export from this module
VariablesToExport = '*'

# Aliases to export from this module
AliasesToExport = '*'

# List of all modules packaged with this module
ModuleList = @()

# List of all files packaged with this module
FileList = @(
	'.\MSBuildMongoDBLogger.psm1'
	'.\MSBuildMongoDBLogger.psd1'
)

# Private data to pass to the module specified in ModuleToProcess
PrivateData = ''

}
