using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoLists
{
    public class UpdateListRequest
    {
        [FromRoute] public string ListId { get; set; }
        [FromBody] public string Title { get; set; }
    }
    
    public class UpdateListResponse
    {
        public string Message { get; set; }
    }

    public class Update : BaseAsyncEndpoint<UpdateListRequest, UpdateListResponse>
    {
        private readonly ITodoListRepository _todoListRepository;

        public Update(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        [HttpPut("{listId}")]
        [SwaggerOperation(
            Summary = "Update a existing List",
            OperationId = "UpdateList",
            Tags = new[] {"Lists"}
        )]
        public override async Task<ActionResult<UpdateListResponse>> HandleAsync([FromRoute] UpdateListRequest request, CancellationToken token)
        {
            var entity = new TodoList(request.ListId, request.Title);

            await _todoListRepository.UpdateAsync(entity, token);

            return Ok(new UpdateListResponse{Message = $"List with {request.ListId} was updated successfully"});
        }
    }
}