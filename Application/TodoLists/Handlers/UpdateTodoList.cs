using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Interfaces;

namespace Application.TodoLists.Handlers
{
    public class UpdateTodoListDto
    {
        public string ListId { get; set; }
        public string Title { get; set; }
    }
    public class UpdateTodoListHandler
    {
        private readonly ITodoListRepository _todoListRepository;

        public UpdateTodoListHandler(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }
        
        public async Task Handle(UpdateTodoListDto request, CancellationToken token)
        {
            var entity = new TodoList(request.ListId, request.Title);

            await _todoListRepository.UpdateAsync(entity, token);
        }
    }
}