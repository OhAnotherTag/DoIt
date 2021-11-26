using System;
using System.Threading;
using System.Threading.Tasks;
using Application.TodoLists.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Command.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoLists
{
    public class Create : BaseEndpoint
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Create(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new List",
            OperationId = "CreateList",
            Tags = new[] {"Lists"}
        )]
        public async Task<ActionResult<BaseResponse>> HandleAsync(
            CreateTodoListCommand request,
            CancellationToken token = default)
        {
            try
            {
                await _commandDispatcher.DispatchAsync(request, token);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ValidationException:
                        return BadRequest(new BaseResponse {Message = ex.Message});
                }
            }

            var baseResponse = new BaseResponse {Message = $"List was created successfully"};
            return Ok(baseResponse);
        }
    }
}