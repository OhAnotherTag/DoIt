using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class SqliteTodoItemRepository : ITodoItemRepository
    {
        private readonly ApplicationContext _context;

        public SqliteTodoItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> GetByIdAsync(int todoId, string listId, CancellationToken token)
        {
            return await _context.Todos
                .Where(t => t.TodoId == todoId && t.ListId == listId)
                .FirstAsync(token);
        }

        public async Task<List<TodoItem>> ListAllAsync(string listId, CancellationToken token)
        {
            return await _context.Todos
                .Where(t => t.ListId == listId)
                .ToListAsync(token);
        }

        public async Task AddAsync(TodoItem item, CancellationToken token)
        {
            _context.Todos.Add(item);
            await _context.SaveChangesAsync(token);
        }

        public async Task UpdateAsync(TodoItem item, CancellationToken token)
        {
            var todo = await this.GetByIdAsync(item.TodoId, item.ListId, token);
            todo.Text = item.Text;
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(TodoItem item, CancellationToken token)
        {
            var todo = await this.GetByIdAsync(item.TodoId, item.ListId, token);
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync(token);
        }
    }
}