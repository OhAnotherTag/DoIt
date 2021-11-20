using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoItems
{
    public class DeleteTodoRequest
    {
        [FromRoute] public string ListId { get; set; }
        [FromRoute] public int TodoId { get; set; }
    }
    
    public class DeleteTodoResponse
    {
        public string Message { get; set; }
    }
    
    public class Delete : BaseAsyncEndpoint<DeleteTodoRequest, DeleteTodoResponse>
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public Delete(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [HttpDelete("{listId}/todos/{TodoId}")]
        [SwaggerOperation(
            Summary = "Delete a existing Todo",
            OperationId = "DeleteTodo",
            Tags = new[] {"Todos"}
        )]
        public override async Task<ActionResult<DeleteTodoResponse>> HandleAsync([FromRoute] DeleteTodoRequest request, CancellationToken token)
        {
            var entity = new TodoItem
            {
                TodoId = request.TodoId
            };
            
            await _todoItemRepository.DeleteAsync(entity, token);
            
            return Ok(new DeleteTodoResponse{Message = $"Todo with TodoId {request.TodoId} was deleted successfully"});
        }
    }
}