/// Copyright 2011 Bas Bossink <bas.bossink@gmail.com>
/// This file is part of MSBuildMongoDBLogger.
///
/// MSBuildMongoDBLogger is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// MSBuildMongoDBLogger is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with MSBuildMongoDBLogger.  If not, see <http://www.gnu.org/licenses/>.

namespace MSBuildMongoDBLogger
{
    using System;

    interface IParameterExtractor
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        void Extract(string parameters);
    }
}
