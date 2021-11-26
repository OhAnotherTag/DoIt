using System.Threading;
using System.Threading.Tasks;
using Application.TodoLists.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Command.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoLists
{
    public class Update : BaseEndpoint
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Update(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut("{listId}")]
        [SwaggerOperation(
            Summary = "Update a existing List",
            OperationId = "UpdateList",
            Tags = new[] {"Lists"}
        )]
        public async Task<ActionResult<BaseResponse>> HandleAsync(
            UpdateTodoListCommand command,
            string listId,
            CancellationToken token)
        {
            command.ListId = listId;
            
            await _commandDispatcher.DispatchAsync(command, token);
            var baseResponse = new BaseResponse
            {
                Message = $"List with {command.ListId} was updated successfully"
            };

            return Ok(baseResponse);
        }
    }
}