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
    public class SqliteTodoListRepository : ITodoListRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public SqliteTodoListRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<TodoList> GetByIdAsync(string id, CancellationToken token)
        {
            const string selectListById =
                @"
                    SELECT *
                    FROM lists
                    WHERE ListId = @ListId;
                 ";
            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var list = await connection.QueryFirstOrDefaultAsync<TodoList>(
                    selectListById,
                    new {ListId = id});
                
                return list;
            }
        }

        public async Task<List<TodoList>> ListAllAsync(int page, int limit, CancellationToken token)
        {
            const string allList =
                @"
                    SELECT *
                    FROM lists
                    LIMIT @Limit OFFSET @Offset;
                 ";

            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var lists = await connection.QueryAsync<TodoList>(
                    allList,
                    new {Limit = limit, Offset = page * limit});
                return Enumerable.ToList(lists);
            }
        }

        public async Task<List<TodoList>> ListAllWithTodosAsync(int page, int limit, CancellationToken token)
        {
            const string allListWithTodos =
                @"
                    SELECT *
                    FROM lists
                    INNER JOIN todos
                        ON lists.ListId = todos.ListId
                    LIMIT @Limit OFFSET @Offset;
                 ";

            var ids = new Dictionary<string, TodoList>();

            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var res = await connection.QueryAsync<TodoList, TodoItem, TodoList>(
                    allListWithTodos,
                    ((list, item) =>
                    {
                        if (!ids.TryGetValue(list.ListId, out var tl))
                        {
                            tl = list;
                            ids.Add(tl.ListId, tl);
                        }

                        tl.Items.Add(item);

                        return tl;
                    }),
                    new {Limit = limit, Offset = page * limit},
                    splitOn: "TodoId");

                return Enumerable.Distinct(res).ToList();
            }
        }

        public async Task AddAsync(TodoList item, CancellationToken token)
        {
            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var rows = await connection.ExecuteAsync(
                    "INSERT INTO lists (ListId, Title) Values (@ListId, @Title);",
                    new {ListId = item.ListId, Title = item.Title,}
                );
            }
        }

        public async Task UpdateAsync(TodoList item, CancellationToken token)
        {
            const string updateQuery = @"
                UPDATE lists
                SET Title = @Title
                WHERE ListId = @ListId;
            ";
            
            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var rows = await connection.ExecuteAsync(
                    updateQuery,
                    new {Title = item.Title, ListId = item.ListId}
                );
            }
        }

        public async Task DeleteAsync(TodoList item, CancellationToken token)
        {
            const string deleteQuery = @"
                DELETE FROM lists
                WHERE ListId = @ListId;
            ";
            
            using (var connection = new SqliteConnection(_databaseConfig.Name))
            {
                var rows = await connection.ExecuteAsync(
                    deleteQuery,
                    new {ListId = item.ListId}
                );
            }
        }
    }
}