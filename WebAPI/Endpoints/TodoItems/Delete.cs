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

        [HttpDelete("{listId}/todos/{todoId}")]
        [SwaggerOperation(
            Summary = "Delete a existing Todo",
            OperationId = "DeleteTodo",
            Tags = new[] {"Todos"}
        )]
        public async Task<ActionResult<BaseResponse>> HandleAsync(
            DeleteTodoCommand command,
            string todoId,
            CancellationToken token)
        {
            await _commandDispatcher.DispatchAsync(command, token);
            return Ok(new BaseResponse{Message = $"Todo with TodoId {command.TodoId} was deleted successfully"});
        }
    }
}