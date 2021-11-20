using System;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Infrastructure.Database
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig _databaseConfig;

        private const string SelectTablesName =
            @"
                SELECT name
                FROM sqlite_master
                WHERE type='table';
            ";

        private const string DbSchema =
            @"
                CREATE TABLE lists (
                  ListId TEXT PRIMARY KEY,
                  Title TEXT NOT NULL UNIQUE
                );
                CREATE TABLE todos (
                  TodoId INTEGER PRIMARY KEY AUTOINCREMENT,
                  Text TEXT NOT NULL,
                  Done INTEGER NOT NULL,
                  ListId TEXT NOT NULL,
                  FOREIGN KEY (ListId)
                    REFERENCES lists (ListId) 
                      ON DELETE CASCADE
                      ON UPDATE NO ACTION
                );
            ";

        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            Console.WriteLine(_databaseConfig.Name);

            using var connection = new SqliteConnection(_databaseConfig.Name);

            var tables = connection.Query<string>(SelectTablesName).ToList();
            tables.ForEach(Console.WriteLine);

            if (tables.Count > 0)
                return;

            var rows = connection.Execute(DbSchema);
            Console.WriteLine($"Rows: {rows}");
        }
    }
}