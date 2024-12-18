using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCouch.CaseStudy.Database
{
    public class DataContext
    {
        private static DataContext _instance;
        private static readonly object _lock = new object(); 

        private readonly SQLiteConnection _connection;
        private readonly string _dbPath;
        private DataContext()
        {
            string systemAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appDataPath = Path.Combine(systemAppDataPath, "HealthCouch");
            _dbPath = Path.Combine(appDataPath, "data.db");

            Directory.CreateDirectory(appDataPath);

            _connection = new SQLiteConnection($"Data Source={_dbPath}");
        }

        public static DataContext Instance
        {
            get
            {
                lock (_lock) 
                {
                    if (_instance == null)
                    {
                        _instance = new DataContext();
                    }
                    return _instance;
                }
            }
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
