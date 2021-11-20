using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoLists
{
    public class DeleteListRequest
    {
        [FromRoute] public string ListId { get; set; }
    }
    
    public class DeleteListResponse
    {
        public string Message { get; set; }
    }
    
    public class Delete : BaseAsyncEndpoint<DeleteListRequest, DeleteListResponse>
    {
        private readonly ITodoListRepository _todoListRepository;

        public Delete(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        [HttpDelete("{listId}")]
        [SwaggerOperation(
            Summary = "Delete a existing List",
            OperationId = "DeleteList",
            Tags = new[] {"Lists"}
        )]
        public override async Task<ActionResult<DeleteListResponse>> HandleAsync([FromRoute] DeleteListRequest request, CancellationToken token)
        {
            var entity = new TodoList(request.ListId, string.Empty);
            
            await _todoListRepository.DeleteAsync(entity, token);

            return Ok(new DeleteListResponse {Message = $"List with {request.ListId} was updated successfully"});
        }
    }
}