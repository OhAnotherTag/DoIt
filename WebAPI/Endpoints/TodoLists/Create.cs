using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoLists
{
    public class CreateListRequest
    {
        [StringLength(50, MinimumLength = 3)]
        [FromBody] public string Title { get; set; }
    }

    public class CreateListResponse
    {
        public string Message { get; set; }
    }
    
    public class Create : BaseAsyncEndpoint<CreateListRequest, CreateListResponse>
    {
        private readonly ITodoListRepository _todoListRepository;

        public Create(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new List",
            OperationId = "CreateList",
            Tags = new[] {"Lists"}
        )]
        public override async Task<ActionResult<CreateListResponse>> HandleAsync([FromRoute] CreateListRequest request, CancellationToken token)
        {
            var id = Guid.NewGuid().ToString();
            var entity = new TodoList(id, request.Title);

            await _todoListRepository.AddAsync(entity, token);
            
            return Ok(new CreateListResponse{Message = $"List with {id} was created successfully"});
        }
    }
}