﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orient.Client;

namespace Orient.Tests.Query
{
    [TestClass]
    public class SqlCreateClusterTests
    {
        [TestMethod]
        public void ShouldCreateCluster()
        {
            using (TestDatabaseContext testContext = new TestDatabaseContext())
            {
                using (ODatabase database = new ODatabase(TestConnection.GLOBAL_TEST_DATABASE_ALIAS))
                {
                    short clusterId1 = database
                        .Create.Cluster("TestCluster1", OClusterType.Physical)
                        .Run();

                    Assert.IsTrue(clusterId1 > 0);

                    short clusterId2 = database
                        .Create.Cluster<TestProfileClass>(OClusterType.Physical)
                        .Run();

                    Assert.AreEqual(clusterId2, clusterId1 + 1);
                }
            }
        }
    }
}
