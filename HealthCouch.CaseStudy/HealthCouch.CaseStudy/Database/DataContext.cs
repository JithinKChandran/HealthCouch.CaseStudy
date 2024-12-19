using System;
using System.Data.SQLite;
using System.IO;

namespace HealthCouch.CaseStudy.Database
{
    public class DataContext
    {
        private SQLiteConnection _connection;
        private readonly string _dbName = "data.db";
        private readonly string _systemAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private readonly string _appDataPath;
        private readonly string _dbPath;

        public DataContext()
        {
            _appDataPath = Path.Combine(_systemAppDataPath, "HealthCouch");
            _dbPath = Path.Combine(_appDataPath, _dbName);

            Directory.CreateDirectory(_appDataPath);
            _connection = new SQLiteConnection($"Data Source={_dbPath}");
        }

        public SQLiteConnection GetConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }
    }
}
