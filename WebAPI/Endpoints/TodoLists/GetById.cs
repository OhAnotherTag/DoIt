using System.Threading;
using System.Threading.Tasks;
using Application.TodoLists.Queries;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Query.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoLists
{
    public class GetByIdListRequest
    {
        [FromRoute] public string ListId { get; set; }
    }
    
    public class GetByIdListResponse
    {
        public TodoListDto Data { get; set; }
        
    }
    
    public class GetById : BaseEndpoint
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public GetById(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{listId}")]
        [SwaggerOperation(
            Summary = "Get List by Id",
            OperationId = "GetByIdList",
            Tags = new[] {"Lists"}
        )]
        public async Task<ActionResult<BaseResponse<TodoListDto>>> HandleAsync(
            GetByIdTodoListQuery query,
            string listId,
            CancellationToken token)
        {
            query.ListId = listId;

            var todos = await _queryDispatcher.QueryAsync(query, token);
            var baseResponse = new BaseResponse<TodoListDto> {Data = todos};
            
            return Ok(baseResponse);
        }   
    }
}