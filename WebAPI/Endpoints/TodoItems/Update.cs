using System;
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
    public class Update : BaseEndpoint
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Update(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut("{listId}/todos/{todoId:int}")]
        [SwaggerOperation(
            Summary = "Update a existing Todo",
            OperationId = "UpdateTodo",
            Tags = new[] {"Todos"}
        )]
        public async Task<ActionResult<BaseResponse>> HandleAsync(
            UpdateTodoCommand command,
            int todoId,
            CancellationToken token)
        {
            command.TodoId = todoId;
            await _commandDispatcher.DispatchAsync(command, token);
            return Ok(new BaseResponse{Message = $"Todo with TodoId {command.TodoId} was updated successfully"});
        }
    }
}