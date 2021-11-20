using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoItems
{
    public class UpdateTodoRequest
    {
        [FromRoute] public string ListId { get; set; }
        [FromRoute] public int TodoId { get; set; }
        [FromBody] public string Text { get; set; }
    }
    
    public class UpdateTodoResponse
    {
        public string Message { get; set; }
    }
    
    public class Update : BaseAsyncEndpoint<UpdateTodoRequest,UpdateTodoResponse>
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public Update(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [HttpPut("{listId}/todos/{TodoId}")]
        [SwaggerOperation(
            Summary = "Update a existing Todo",
            OperationId = "UpdateTodo",
            Tags = new[] {"Todos"}
        )]
        public override async Task<ActionResult<UpdateTodoResponse>> HandleAsync([FromRoute] UpdateTodoRequest request, CancellationToken token)
        {
            var entity = new TodoItem
            {
                TodoId = request.TodoId,
                Text = request.Text
            };
            
            await _todoItemRepository.UpdateAsync(entity, token);
            
            return Ok(new UpdateTodoResponse{Message = $"Todo with TodoId {request.TodoId} was updated successfully"});
        }
    }
}