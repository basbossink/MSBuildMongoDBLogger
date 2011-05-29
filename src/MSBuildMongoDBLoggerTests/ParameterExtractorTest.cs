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
namespace MSBuildMongoDBLogger.Test
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ParameterExtractorTest
    {
        [Test]
        public void TestSyntaxCheckingParameters()
        {
            Assert.Throws<ArgumentException>(() => new ParameterExtractor().Extract("connection=mongodb://localhost;tneohu"));
            Assert.Throws<ArgumentException>(() => new ParameterExtractor().Extract("connection="));
        }

        [Test]
        public void TestConnectionStringSyntaxChecking()
        {
            Assert.Throws<ArgumentException>(() => new ParameterExtractor().Extract("connection=mngodb://localhost"));
            Assert.Throws<ArgumentException>(() => new ParameterExtractor().Extract("connection=mongodb://localhost?harry=ou"));
        }

        [Test]
        public void TestConnectionString()
        {
            Func<ParameterExtractor,string> provider = s => s.ConnectionString;
            AssertExtraction("connection=mongodb://localhost", "mongodb://localhost", provider);
            AssertExtraction("connection=mongodb://localhost;database=fred", "mongodb://localhost", provider);
            AssertExtraction("connection=mongodb://localhost:1337", "mongodb://localhost:1337", provider);
            AssertExtraction("connection=mongodb://localhost,ikkyo:1234,nikkyo:5678",
                "mongodb://localhost,ikkyo:1234,nikkyo:5678", provider);
            AssertExtraction("connection=mongodb://localhost/?safe=true", 
                "mongodb://localhost/?safe=true", provider);
            AssertExtraction("connection=mongodb://localhost/fred?safe=true", 
                "mongodb://localhost/fred?safe=true", provider);
        }

        [Test]
        public void TestDatabaseName()
        {
            Func<ParameterExtractor,string> provider = s => s.DatabaseName;
            AssertExtraction("connection=mongodb://localhost/fred", "fred", provider);
            AssertExtraction("connection=mongodb://localhost/fred?safe=true", "fred", provider);
            AssertExtraction("database=fred", "fred", provider);
            AssertExtraction("connection=mongodb://localhost;database=fred", "fred", provider);
        }

        [Test]
        public void TestDifferentDatabaseNames()
        {
            Assert.Throws<ArgumentException>(() => new ParameterExtractor().Extract("connection=mongodb://localhost/barney;database=fred"));
        }
        
        public static void AssertExtraction(
            string testData, 
            string expected,
            Func<ParameterExtractor, string> actualProvider)
        {
            var sut = new ParameterExtractor();
            sut.Extract(testData);
            Assert.AreEqual(expected, actualProvider(sut));
        }
    }
}