using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoItems
{
    public class CreateTodoRequest
    {
        [FromRoute] public string ListId { get; set; }
        [FromBody] public string Text { get; set; }
    }
    
    public class CreateTodoResponse
    {
        public string Message { get; set; }
    }
    
    public class Create : BaseAsyncEndpoint<CreateTodoRequest, CreateTodoResponse>
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public Create(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [HttpPost("{listId}/todos")]
        [SwaggerOperation(
            Summary = "Creates a new Todo",
            OperationId = "CreateTodo",
            Tags = new[] {"Todos"}
        )]
        public override async Task<ActionResult<CreateTodoResponse>> HandleAsync([FromRoute] CreateTodoRequest request, CancellationToken token)
        {
            var entity = new TodoItem
            {
                ListId = request.ListId,
                Text = request.Text,
            };
            
            // await _todoItemValidator.ValidateAndThrowAsync(entity, token);

            await _todoItemRepository.AddAsync(entity, token);

            return Ok(new CreateTodoResponse{Message = $"Todo with ListId {request.ListId} was created successfully"});
        }
    }
}