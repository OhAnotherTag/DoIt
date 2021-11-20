using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Interfaces;

namespace Application.TodoLists.Handlers
{
    public class CreateTodoListDto
    {
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
    }

    public class CreateTodoListHandler
    {
        private readonly ITodoListRepository _todoListRepository;

        public CreateTodoListHandler(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public async Task<string> Handle(CreateTodoListDto request, CancellationToken token)
        {
            var id = Guid.NewGuid().ToString();
            var entity = new TodoList(id, request.Title);

            await _todoListRepository.AddAsync(entity, token);
            
            return id;
        }
    }
}