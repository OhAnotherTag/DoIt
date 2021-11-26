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
            string listId,
            CancellationToken token)
        {
            command.TodoId = todoId;
            command.ListId = listId;
            await _commandDispatcher.DispatchAsync(command, token);
            var value = new BaseResponse
            {
                Message = $"Todo with TodoId {command.TodoId} was updated successfully"
            };
            return Ok(value);
        }
    }
}