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
    using System.Text.RegularExpressions;
    using System.Linq;
    using System;

    public class ParameterExtractor : MSBuildMongoDBLogger.IParameterExtractor
    {
        private const char ParameterDelimiter = ';';
        private const string KeyValuePairDelimiter = "=";
        private const string ConnectionStringKeyToken = "connection";
        private const string DatabaseNameKeyToken = "database";
        private const string DefaultDatabaseName = "msbuildlogs";
 
        private static readonly Regex ConnectionStringTokenizer = 
            new Regex(@"^(?<hosts>mongodb://[^/\?=]+)(/(?<database>[^/\?=]+)?(?<options>\?.+)?)?$");

        private static readonly Regex ParameterSyntaxChecker =
            new Regex(@"(^(connection\s*=\s*[^;]+)(;database\s*=\s*[^;]+)?$)|(^(database\s*=\s*[^;]+)$)");

        public void Extract(string parameters)
        {
            if (string.IsNullOrEmpty(parameters))
            {
                DatabaseName = DefaultDatabaseName;
                return;
            }
            ValidateSyntax(parameters);
            string[] pars = parameters.Split(ParameterDelimiter);
            ConnectionString = Extract(ConnectionStringKeyToken, pars);
            DatabaseName = ExtractDatabaseName(pars);
        }

        private string ExtractDatabaseName(string[] pars)
        {
            var dbNameFromPars = Extract(DatabaseNameKeyToken, pars);
            var dbNameFromConnectionString = ExtractDatabaseNameFromConnectionString();
            if (string.IsNullOrEmpty(dbNameFromConnectionString) &&
                string.IsNullOrEmpty(dbNameFromPars))
            {
                return DefaultDatabaseName;
            }
            if (string.IsNullOrEmpty(dbNameFromPars))
            {
                return dbNameFromConnectionString;
            }
            if (string.IsNullOrEmpty(dbNameFromConnectionString))
            {
                return dbNameFromPars;
            }
            if (!dbNameFromPars.Equals(dbNameFromConnectionString, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException(
                    string.Format("Database name in the supplied connection string {0} is not equal to the database parameter value {1}",
                    dbNameFromConnectionString, dbNameFromPars));
            }
            return dbNameFromPars;
        }

        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }

        private void ValidateSyntax(string parameters)
        {
            if (!ParameterSyntaxChecker.IsMatch(parameters))
            {
                    throw new ArgumentException(
                        string.Format("Syntax error in {0}, parameters should be of the form: "+
                                      "connection=<connectionstring>[;database=<database name>]|database=<database name>", 
                        parameters));
            }
        }

        private string Extract(string keyToken, string[] pars)
        {
            var par = pars.FirstOrDefault(s => s.StartsWith(keyToken));
            if (par != null)
            {
                var val = par.Substring(par.IndexOf(KeyValuePairDelimiter)+1);
                return val;
            }
            return null;
        }

        private string ExtractDatabaseNameFromConnectionString()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                return null;
            }
            if (!ConnectionStringTokenizer.IsMatch(ConnectionString))
            {
                throw new ArgumentException(
                    string.Format("syntax error in connection string {0}, the connection string should be of the form: " +
                    "mongodb://[username:password@]host1[:port][/[database][?options]]", ConnectionString));
            }
            return ConnectionStringTokenizer.Match(ConnectionString).Groups["database"].Value;
        }
    }
}