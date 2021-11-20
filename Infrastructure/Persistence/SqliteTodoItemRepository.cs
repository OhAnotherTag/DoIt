using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.Data.Sqlite;
using SharedKernel.Interfaces;

namespace Infrastructure.Persistence
{
    public class SqliteTodoItemRepository : ITodoItemRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public SqliteTodoItemRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<TodoItem> GetByIdAsync(int id, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TodoItem>> ListAllAsync(string listId, CancellationToken token)
        {
            const string selectItems = @"
                    SELECT *
                    FROM todos
                    WHERE ListId = @ListId
                    LIMIT 10; 
                ";

            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var todos = await connection.QueryAsync<TodoItem>(selectItems, new {ListId = listId});
                return Enumerable.ToList(todos);
            }
        }

        public async Task AddAsync(TodoItem item, CancellationToken token)
        {
            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var rows = await connection.ExecuteAsync(
                    "INSERT INTO todos (Text, Done, ListId) Values (@Text, @Done, @ListId);",
                    new {Text = item.Text, Done = item.Done, ListId = item.ListId}
                );
            }
        }

        public async Task UpdateAsync(TodoItem item, CancellationToken token)
        {
            const string updateQuery = @"
                UPDATE todos
                SET Done = @Done,
                  Text = @Text
                WHERE TodoId = @TodoId;
            ";
            
            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var rows = await connection.ExecuteAsync(
                    updateQuery,
                    new {Done = item.Done, Text = item.Text, TodoId = item.TodoId}
                );
            }
        }

        public async Task DeleteAsync(TodoItem item, CancellationToken token)
        {
            const string deleteQuery = @"
                DELETE FROM todos
                WHERE TodoId = @TodoId;
            ";
            
            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var rows = await connection.ExecuteAsync(
                    deleteQuery,
                    new {TodoId = item.TodoId}
                );
            }
        }
    }
}