using System;
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
    public class Delete : BaseEndpoint
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Delete(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpDelete("{listId}")]
        [SwaggerOperation(
            Summary = "Delete a existing List",
            OperationId = "DeleteList",
            Tags = new[] {"Lists"}
        )]
        public async Task<ActionResult<BaseResponse>> HandleAsync(
            string listId,
            CancellationToken token)
        {
            var command = new DeleteTodoListCommand
            {
                ListId = listId
            };

            try
            {
                await _commandDispatcher.DispatchAsync(command, token);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            var baseResponse = new BaseResponse
            {
                Message = $"List with {command.ListId} was updated successfully"
            };
            return Ok(baseResponse);
        }
    }
}