﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orient.Client;

namespace Orient.Tests.Query
{
    [TestClass]
    public class SqlCreateDocumentTests
    {
        [TestMethod]
        public void ShouldCreateDocumentFromDocument()
        {
            using (TestDatabaseContext testContext = new TestDatabaseContext())
            {
                using (ODatabase database = new ODatabase(TestConnection.GLOBAL_TEST_DATABASE_ALIAS))
                {
                    // prerequisites
                    database
                        .Create.Class("TestClass")
                        .Run();

                    ODocument document = new ODocument();
                    document.OClassName = "TestClass";
                    document
                        .SetField("foo", "foo string value")
                        .SetField("bar", 12345);

                    ODocument createdDocument = database
                        .Create.Document(document)
                        .Run();

                    Assert.IsTrue(createdDocument.ORID != null);
                    Assert.AreEqual(createdDocument.OClassName, "TestClass");
                    Assert.AreEqual(createdDocument.GetField<string>("foo"), document.GetField<string>("foo"));
                    Assert.AreEqual(createdDocument.GetField<int>("bar"), document.GetField<int>("bar"));
                }
            }
        }

        [TestMethod]
        public void ShouldCreateDocumentFromObject()
        {
            using (TestDatabaseContext testContext = new TestDatabaseContext())
            {
                using (ODatabase database = new ODatabase(TestConnection.GLOBAL_TEST_DATABASE_ALIAS))
                {
                    // prerequisites
                    database
                        .Create.Class<TestProfileClass>()
                        .Run();

                    TestProfileClass profile = new TestProfileClass();
                    profile.Name = "Johny";
                    profile.Surname = "Bravo";

                    TestProfileClass createdObject = database
                        .Create.Document(profile)
                        .Run<TestProfileClass>();

                    Assert.IsTrue(createdObject.ORID != null);
                    Assert.AreEqual(createdObject.OClassName, typeof(TestProfileClass).Name);
                    Assert.AreEqual(createdObject.Name, profile.Name);
                    Assert.AreEqual(createdObject.Surname, profile.Surname);
                }
            }
        }

        [TestMethod]
        public void ShouldCreateDocumentClassSet()
        {
            using (TestDatabaseContext testContext = new TestDatabaseContext())
            {
                using (ODatabase database = new ODatabase(TestConnection.GLOBAL_TEST_DATABASE_ALIAS))
                {
                    // prerequisites
                    database
                        .Create.Class("TestClass")
                        .Run();

                    ODocument document = new ODocument();
                    document.OClassName = "TestClass";
                    document
                        .SetField("foo", "foo string value")
                        .SetField("bar", 12345);

                    ODocument createdDocument = database
                        .Create.Document("TestClass")
                        .Set(document)
                        .Run();

                    Assert.IsTrue(createdDocument.ORID != null);
                    Assert.AreEqual(createdDocument.OClassName, "TestClass");
                    Assert.AreEqual(createdDocument.GetField<string>("foo"), document.GetField<string>("foo"));
                    Assert.AreEqual(createdDocument.GetField<int>("bar"), document.GetField<int>("bar"));
                }
            }
        }
    }
}
