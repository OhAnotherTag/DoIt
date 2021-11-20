using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Interfaces;

namespace Application.TodoLists.Handlers
{
    public class GetAllTodoListQuery
    {
        public bool Todos { get; set; }
        [Range(0, 50)]
        public int Limit { get; set; } = 5;
        public int Page { get; set; } = 0;
    }

    public class GetAllTodoListHandler
    {
        private readonly ITodoListRepository _todoListRepository;

        public GetAllTodoListHandler(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public async Task<List<TodoListDto>> Handle(GetAllTodoListQuery request, CancellationToken token)
        {
            List<TodoList> list;

            if (request.Todos)
            {
                list = await _todoListRepository.ListAllWithTodosAsync(request.Page, request.Limit, token);
            }
            else
            {
                list = await _todoListRepository.ListAllAsync(request.Page, request.Limit, token);
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