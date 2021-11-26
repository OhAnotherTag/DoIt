using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Query.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoLists.Queries
{
    public class GetAllTodoListQuery : IQuery<List<TodoListDto>>
    {
        public bool Todos { get; set; }
        public int Limit { get; set; } = 5;
        public int Page { get; set; } = 0;
    }
    
    public class GetAllTodoListHandler : IQueryHandler<GetAllTodoListQuery, List<TodoListDto>>
    {
        private readonly ITodoListRepository _todoListRepository;

        public GetAllTodoListHandler(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public async Task<List<TodoListDto>> HandleAsync(GetAllTodoListQuery query, CancellationToken token = default)
        {
            List<TodoList> list;

            if (query.Todos)
            {
                list = await _todoListRepository.ListAllWithTodosAsync(query.Page, query.Limit, token);
            }
            else
            {
                list = await _todoListRepository.ListAllAsync(query.Page, query.Limit, token);
            }
            
            return list.Select(l => new TodoListDto
            {
                ListId = l.ListId,
                Title = l.Title,
                Items = l.Items
            }).ToList();
        }
    }
}