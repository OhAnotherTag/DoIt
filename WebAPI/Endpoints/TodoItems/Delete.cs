using System.Threading;
using System.Threading.Tasks;
using Application.TodoItems.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Command.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoItems
{
    public class Delete : BaseEndpoint
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Delete(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpDelete("{listId}/todos/{todoId:int}")]
        [SwaggerOperation(
            Summary = "Delete a existing Todo",
            OperationId = "DeleteTodo",
            Tags = new[] {"Todos"}
        )]
        public async Task<ActionResult<BaseResponse>> HandleAsync(
            int todoId,
            string listId,
            CancellationToken token)
        {
            DeleteTodoCommand command = new() {TodoId = todoId, ListId = listId};
            
            await _commandDispatcher.DispatchAsync(command, token);
            var value = new BaseResponse
            {
                Message = $"Todo with TodoId {command.TodoId} was deleted successfully"
            };
            return Ok(value);
        }
    }
}