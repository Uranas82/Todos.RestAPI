using Microsoft.AspNetCore.Mvc;
using Persistence.Models.ReadModels;
using RestAPI.Models.RequestModels;
using RestAPI.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Repositories;
using RestAPI.Options;
using Microsoft.Extensions.Options;
using RestAPI.Attributes;
using System.Security.Cryptography;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodosController : ControllerBase
    {
        private readonly ITodosRepository _todosRepository;
        private readonly FavQ _favQSettings;

        public TodosController(ITodosRepository todosRepository, IOptions<FavQ> favQSettings)
        {
            _todosRepository = todosRepository;
            _favQSettings = favQSettings.Value;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<ActionResult<IEnumerable<TodoResponse>>> CreateApiKey()
        {
            var userId = (Guid)HttpContext.Items["userId"];
                
            var todos = await _todosRepository.GetTodosAsync();

            return new ActionResult<IEnumerable<TodoResponse>>(todos.Select(todo => todo.MapToTodoResponse()));
        }

        [HttpPost]
        [Route("apiKey")]
        public async Task<ActionResult<IEnumerable<TodoResponse>>> SignUp()
        {
            var userId = (Guid)HttpContext.Items["userId"];

            var todos = await _todosRepository.GetTodosAsync();

            return new ActionResult<IEnumerable<TodoResponse>>(todos.Select(todo => todo.MapToTodoResponse()));
        }

        [HttpGet]
        [ApiKey]
        public async Task<ActionResult<IEnumerable<TodoResponse>>> GetTodos()
        {
            var userId = (Guid)HttpContext.Items["userId"];

            var test = _favQSettings.DefaultEmail;

            var todos = await _todosRepository.GetTodosAsync();

            return new ActionResult<IEnumerable<TodoResponse>>(todos.Select(todo => todo.MapToTodoResponse()));
                     
        }      

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TodoResponse>> Get(Guid id)
        {
            
            
            var todoItem = await _todosRepository.GetTodoAsync(id);

            if (todoItem is null)
            {
                return NotFound($"Todo item with specified id: '{id}' does not exist");
            }
            return Ok(todoItem.MapToTodoResponse());
        }

    
        [HttpPost]
        public async Task<ActionResult<TodoResponse>> AddTodo(AddTodoRequest request)
        {
            var todoItemReadModel = new TodoReadModel
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Difficulty = request.Difficulty,
                IsDone = false,
                DateCreated = DateTime.Now
            };
            await _todosRepository.SaveOrUpdateAsync(todoItemReadModel);

            return CreatedAtAction(nameof(Get), new { Id = todoItemReadModel.Id }, todoItemReadModel.MapToTodoResponse());
        }

     
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<TodoResponse>> UpdateTodo(Guid id, UpdateTodoRequest request)
        {
            var todoItem = await _todosRepository.GetTodoAsync(id);
            if (todoItem is null)
            {
                return NotFound($"Todo item with specified id: '{id}' does not exist");
            }
                      
            todoItem.Title = request.Title;
            todoItem.Description = request.Description;
            // todoItem.IsDone = request.IsDone;

            await _todosRepository.SaveOrUpdateAsync(todoItem);

            return todoItem.MapToTodoResponse();
        }

        [HttpPatch]
        [Route("{id}/toggleStatus")]
        public async Task<ActionResult<TodoResponse>> UpdateStatus(Guid id)
        {
            var todoItem = await _todosRepository.GetTodoAsync(id);

            if (todoItem is null)
            {
                return NotFound($"Todo item with specified id: '{id}' does not exist");
            }

            todoItem.IsDone = !todoItem.IsDone;

            await _todosRepository.SaveOrUpdateAsync(todoItem);

            return todoItem.MapToTodoResponse();
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var todoItem = await _todosRepository.GetTodoAsync(id);

            if (todoItem is null)
            {
                return NotFound($"Todo item with id: '{id}' does not exist");
            }

            await _todosRepository.DeleteAsync(id);

            return NoContent();
        }

        private string GenerateApiKey()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return Convert.ToBase64String(key);
        }
    }
      
}
