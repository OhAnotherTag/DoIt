using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.TodoItems.Queries;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Command.Interfaces;
using SharedKernel.Query.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoItems
{

    public class GetAll : BaseEndpoint
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public GetAll(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }
        
        [HttpGet("{listId}/todos")]
        [SwaggerOperation(
            Summary = "Get all Todos of a given List",
            OperationId = "GetAllTodo",
            Tags = new[] {"Todos"}
        )]
        public async Task<ActionResult<BaseResponse<List<TodoDto>>>> HandleAsync(
            string listId,
            CancellationToken token)
        {
            var query = new GetAllTodosQuery
            {
                ListId = listId
            };
            
            var todos = await _queryDispatcher.QueryAsync(query, token);

            return Ok(new BaseResponse<List<TodoDto>>{Data = todos});
        }
    }
}