using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Interfaces;

namespace Application.TodoLists.Handlers
{
    public class DeleteTodoListDto
    {
        public string ListId { get; init; } = string.Empty;
    }

    public class DeleteTodoListHandler
    {
        private readonly ITodoListRepository _todoListRepository;

        public DeleteTodoListHandler(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }
        
        public async Task Handle(DeleteTodoListDto request, CancellationToken token)
        {
            var entity = new TodoList(request.ListId, string.Empty);
            
            await _todoListRepository.DeleteAsync(entity, token);
        }
    }
}