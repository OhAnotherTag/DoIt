using System;
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
    public class SqliteTodoListRepository : ITodoListRepository
    {
        private readonly ApplicationContext _context;


        public SqliteTodoListRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TodoList> GetByIdAsync(string id, CancellationToken token)
        {
            return await _context.Lists.FirstAsync(l => l.ListId == id,cancellationToken: token);
        }

        public async Task<List<TodoList>> ListAllAsync(int page, int limit, CancellationToken token)
        {
            return await _context.Lists
                .Take(limit)
                .Skip(limit * page)
                .ToListAsync(cancellationToken: token);
        }

        public async Task<List<TodoList>> ListAllWithTodosAsync(int page, int limit, CancellationToken token)
        {
            return await _context.Lists
                .Include(l => l.Items)
                .Take(limit).Skip(limit * page)
                .ToListAsync(cancellationToken: token);
        }

        public async Task AddAsync(TodoList item, CancellationToken token)
        {
            _context.Lists.Add(item);
            
            await _context.SaveChangesAsync(token);
        }

        public async Task UpdateAsync(TodoList item, CancellationToken token)
        {
            var list = await _context.Lists.FirstAsync(l => l.ListId == item.ListId, token);
            list.Title = item.Title;
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(TodoList item, CancellationToken token)
        {
            var list = await _context.Lists.FirstAsync(l => l.ListId == item.ListId, token);
            _context.Lists.Remove(list);
            await _context.SaveChangesAsync(token);
        }
    }
}