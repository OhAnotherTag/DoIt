using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.TodoLists.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Query.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoLists
{
    public class GetAll : BaseEndpoint
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public GetAll(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all Lists",
            OperationId = "GetAllList",
            Tags = new[] {"Lists"}
        )]
        public async Task<ActionResult<BaseResponse<List<TodoListDto>>>> HandleAsync(
            [FromQuery] GetAllTodoListQuery query,
            CancellationToken token)
        {
            var res = await _queryDispatcher.QueryAsync(query, token);

            var baseResponse = new BaseResponse<List<TodoListDto>>{Data = res};
            return Ok(baseResponse);
        }
    }       
}