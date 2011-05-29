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
    using Microsoft.Build.Utilities;
    using Microsoft.Build.Framework;
    using MongoDB.Driver;
    using MongoDB.Driver.Connections;

    public class MongoDBLogger : Logger
    {

        public override void Initialize(Microsoft.Build.Framework.IEventSource eventSource)
        {
            var extractor = new ParameterExtractor();
            extractor.Extract(Parameters);
            var mongo = string.IsNullOrEmpty(extractor.ConnectionString) ? new Mongo() : new Mongo(extractor.ConnectionString);
            eventSource.ProjectStarted += ProjectStarted;
        }
        
        private void ProjectStarted(object sender, ProjectStartedEventArgs e)
        {

        }
    }
}