using Orient.Client;

namespace Orient.Tests
{
    public class TestConnection
    {
        private const string HOSTNAME = "127.0.0.1";
        private const int PORT = 2434;
        private const string USERNAME = "admin";
        private const string PASSWORD = "admin";

        private const string ROOT_USER_NAME = "root";
        private const string ROOT_USER_PARSSWORD = "UR4swimalne!";
        private readonly OServer _server;
        private bool _ssl;

        public static int GlobalTestDatabasePoolSize => 3;
        public const string GLOBAL_TEST_DATABASE_NAME = "globalTestDatabaseForNetDriver001";
        public ODatabaseType GlobalTestDatabaseType { get; private set; }
        public const string GLOBAL_TEST_DATABASE_ALIAS = "globalTestDatabaseForNetDriver001Alias";

        public TestConnection(bool ssl=false)
        {
            _ssl = ssl;
            _server = new OServer(HOSTNAME, PORT, ROOT_USER_NAME, ROOT_USER_PARSSWORD, ssl);
            GlobalTestDatabaseType = ODatabaseType.Graph;
        }

        public void CreateTestDatabase()
        {
            DropTestDatabase();
            _server.CreateDatabase(GLOBAL_TEST_DATABASE_NAME, GlobalTestDatabaseType, OStorageType.Memory);
        }

        public void DropTestDatabase()
        {
            if (_server.DatabaseExist(GLOBAL_TEST_DATABASE_NAME, OStorageType.Memory))
            {
                _server.DropDatabase(GLOBAL_TEST_DATABASE_NAME, OStorageType.Memory);
            }
        }

        public void CreateTestPool()
        {
            OClient.CreateDatabasePool(
                HOSTNAME,
                PORT,
                GLOBAL_TEST_DATABASE_NAME,
                GlobalTestDatabaseType,
                USERNAME,
                PASSWORD,
                GlobalTestDatabasePoolSize,
                GLOBAL_TEST_DATABASE_ALIAS
            );
        }

        public void DropTestPool()
        {
            OClient.DropDatabasePool(GLOBAL_TEST_DATABASE_ALIAS);
        }

        public OServer Server => _server;
    }
}
