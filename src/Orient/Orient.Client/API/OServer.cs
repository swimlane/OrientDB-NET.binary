using System;
using System.Collections.Generic;
using Orient.Client.Protocol;
using Orient.Client.Protocol.Operations;

namespace Orient.Client
{
    public class OServer : IDisposable
    {
        private readonly Connection _connection;

        public OServer(string hostname, int port, string userName, string userPassword, bool ssl=false)
        {
            _connection = new Connection(hostname, port, userName, userPassword, ssl);
        }

        public bool CreateDatabase(string databaseName, ODatabaseType databaseType, OStorageType storageType)
        {
            var operation = new DbCreate
            {
                DatabaseName = databaseName,
                DatabaseType = databaseType,
                StorageType = storageType
            };

            var document = _connection.ExecuteOperation(operation);
            return document.GetField<bool>("IsCreated");
        }

        public bool DatabaseExist(string databaseName, OStorageType storageType)
        {
            var operation = new DbExist
            {
                DatabaseName = databaseName,
                StorageType = storageType
            };

            var document = _connection.ExecuteOperation(operation);

            return document.GetField<bool>("Exists");
        }

        public void DropDatabase(string databaseName, OStorageType storageType)
        {
            var operation = new DbDrop
            {
                DatabaseName = databaseName,
                StorageType = storageType
            };

            var document = _connection.ExecuteOperation(operation);
        }

        #region Configuration

        public string ConfigGet(string key)
        {
            ConfigGet operation = new ConfigGet();
            operation.ConfigKey = key;
            ODocument document = _connection.ExecuteOperation(operation);
            return document.GetField<string>(key);
        }

        public bool ConfigSet(string configKey, string configValue)
        {
            ConfigSet operation = new ConfigSet();
            operation.Key = configKey;
            operation.Value = configValue;
            ODocument document = _connection.ExecuteOperation(operation);

            return document.GetField<bool>("IsCreated");
        }

        public Dictionary<string, string> ConfigList()
        {
            ConfigList operation = new ConfigList();
            ODocument document = _connection.ExecuteOperation(operation);
            return document.GetField<Dictionary<string, string>>("config");
        }

        #endregion

        public Dictionary<string, string> Databases()
        {
            Dictionary<string, string> returnValue = new Dictionary<string, string>();
            DBList operation = new DBList();
            ODocument document = _connection.ExecuteOperation(operation);
            string[] databases = document.GetField<string>("databases").Split(',');
            foreach (var item in databases)
            {
                string[] keyValue = item.Split(':');
                returnValue.Add(keyValue[0], keyValue[1] + ":" + keyValue[2]);
            }
            return returnValue;
        }
        
        
        public void Close()
        {
            _connection.Dispose();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
