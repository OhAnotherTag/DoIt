using System.Threading;
using System.Threading.Tasks;
using Application.TodoItems.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Command.Interfaces;
using SharedKernel.Query.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoItems
{

    public class Create : BaseEndpoint
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Create(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        
        [HttpPost("{listId}/todos")]
        [SwaggerOperation(
            Summary = "Creates a new Todo",
            OperationId = "CreateTodo",
            Tags = new[] {"Todos"}
        )]
        public async Task<ActionResult<BaseResponse>> HandleAsync(
            CreateTodoCommand command,
            string listId,
            CancellationToken token)
        {
            command.ListId = listId;
            await _commandDispatcher.DispatchAsync(command, token);
            return Ok(new BaseResponse{Message = $"Todo with ListId {command.ListId} was created successfully"});
        }
    }
}