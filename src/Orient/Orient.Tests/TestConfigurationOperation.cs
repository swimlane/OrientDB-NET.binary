using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orient.Client;

namespace Orient.Tests
{
    [TestClass]
    public class TestConfigurationOperation
    {
        [TestMethod]
        public void TestConfigGet()
        {
            var connection = new TestConnection();
            OServer server = connection.Server;
            string value = server.ConfigGet("db.document.serializer");
            Assert.AreEqual("ORecordSerializerBinary", value);
        }
        [TestMethod]
        public void TestConfigList()
        {
            var connection = new TestConnection();
            OServer server = connection.Server;
            Dictionary<string, string> config = server.ConfigList();
            Assert.IsTrue(config.Count > 0);
        }
        [TestMethod]
        public void TestConfigSet()
        {
            var connection = new TestConnection();
            OServer server = connection.Server;
            // Only Set existing keys
            // Don't create new one
            bool IsCreated = server.ConfigSet("network.retry", "6");
            string loadedValue = server.ConfigGet("network.retry");
            Assert.IsTrue(IsCreated);
            Assert.AreEqual("6", loadedValue);
        }
    }
}
