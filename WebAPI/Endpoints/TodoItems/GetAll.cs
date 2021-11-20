using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.TodoItems
{
    public class GetAllTodoRequest
    {
        [FromRoute] public string ListId { get; set; }
    }
    
    public class GetAllTodoResponse
    {
        public List<TodoItemDto> Data { get; set; }
    }
    
    public class GetAll : BaseAsyncEndpoint<GetAllTodoRequest, GetAllTodoResponse>
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public GetAll(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        
        [HttpGet("{listId}/todos")]
        [SwaggerOperation(
            Summary = "Get all Todos of a given List",
            OperationId = "GetAllTodo",
            Tags = new[] {"Todos"}
        )]
        public override async Task<ActionResult<GetAllTodoResponse>> HandleAsync([FromRoute] GetAllTodoRequest request, CancellationToken token)
        {
            var todos = await _todoItemRepository.ListAllAsync(request.ListId, token);
            var response = todos.Select(todo => new TodoItemDto
            {
                TodoId = todo.TodoId,
                ListId = todo.ListId,
                Text = todo.Text,
                Done = todo.Done
            }).ToList();

            return Ok(new GetAllTodoResponse{Data = response});
        }
    }
}