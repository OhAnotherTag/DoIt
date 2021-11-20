using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoLists
{
    public class GetAllListRequest  
    {   
        [FromQuery] public bool Todos { get; set; }
        [FromQuery] public int Limit { get; set; } = 5;
        [FromQuery] public int Page { get; set; } = 0;
    }
        
    public class GetAllListResponse
    {
        public List<TodoListDto> Data { get; set; }
            
    }   
    
    public class GetAll : BaseAsyncEndpoint<GetAllListRequest,GetAllListResponse>
    {
        private readonly ITodoListRepository _todoListRepository;

        public GetAll(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all Lists",
            OperationId = "GetAllList",
            Tags = new[] {"Lists"}
        )]
        public override async Task<ActionResult<GetAllListResponse>> HandleAsync([FromRoute] GetAllListRequest request, CancellationToken token)
        {
            List<TodoList> list;

            if (request.Todos)
            {
                list = await _todoListRepository.ListAllWithTodosAsync(request.Page, request.Limit, token);
            }
            else
            {
                list = await _todoListRepository.ListAllAsync(request.Page, request.Limit, token);
            }
            
            var res = list.Select(l => new TodoListDto
            {
                ListId = l.ListId,
                Title = l.Title,
                Items = l.Items
            }).ToList();

            return Ok(new GetAllListResponse{Data = res});
        }
    }       
}