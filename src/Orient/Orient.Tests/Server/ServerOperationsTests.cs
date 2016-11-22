﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orient.Client;

namespace Orient.Tests.Server
{
    [TestClass]
    public class ServerOperationsTests
    {
        [TestMethod]
        public void ShouldCreateAndDeleteDatabase()
        {
            string databaseName = "thisIsTestDatabaseForNetDriver";
            var connection = new TestConnection();
            OServer server = connection.Server;

            bool exists = server.DatabaseExist(databaseName, OStorageType.PLocal);

            if (exists)
            {
                server.DropDatabase(databaseName, OStorageType.PLocal);

                exists = server.DatabaseExist(databaseName, OStorageType.PLocal);
            }

            Assert.AreEqual(exists, false);

            if (!exists)
            {
                bool isCreated = server.CreateDatabase(databaseName, ODatabaseType.Graph, OStorageType.PLocal);

                Assert.AreEqual(isCreated, true);

                if (isCreated)
                {
                    server.DropDatabase(databaseName, OStorageType.PLocal);

                    exists = server.DatabaseExist(databaseName, OStorageType.PLocal);

                    Assert.AreEqual(exists, false);
                }
            }
        }

        [TestMethod]
        public void TestDbList()
        {
            var connection = new TestConnection();
            OServer server = connection.Server;
            Dictionary<string, string> databases = server.Databases();
            Assert.IsTrue(databases.Count > 0);
        }
    }
}
