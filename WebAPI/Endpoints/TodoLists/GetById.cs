using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
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
    
    public class GetById : BaseAsyncEndpoint<GetByIdListRequest, GetByIdListResponse>
    {
        private readonly ITodoListRepository _todoListRepository;

        public GetById(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        [HttpGet("{listId}")]
        [SwaggerOperation(
            Summary = "Get List by Id",
            OperationId = "GetByIdList",
            Tags = new[] {"Lists"}
        )]
        public override async Task<ActionResult<GetByIdListResponse>> HandleAsync([FromRoute] GetByIdListRequest request, CancellationToken token)
        {   
            var todos = await _todoListRepository.GetByIdAsync(request.ListId, token);
            
            return Ok(new GetByIdListResponse
            {
                Data = new TodoListDto
                {
                    ListId = todos.ListId,
                    Title = todos.Title,
                    Items = todos.Items
                }
            });
        }   
    }
}