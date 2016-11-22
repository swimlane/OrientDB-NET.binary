using System;

namespace Orient.Tests
{
    public class TestDatabaseContext : IDisposable
    {
        private readonly TestConnection _connection;

        public TestDatabaseContext()
        {
            _connection = new TestConnection();
            _connection.CreateTestDatabase();
            _connection.CreateTestPool();
        }

        public void Dispose()
        {
            _connection.DropTestPool();
            _connection.DropTestDatabase();
        }
    }
}
