using Microsoft.AspNetCore.Mvc;
using Persistence.Models.ReadModels;
using RestAPI.Models;
using RestAPI.Models.RequestModels;
using RestAPI.Models.ResponseModels;
using RestAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Repositories;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodosController : ControllerBase     
    {
        private readonly ITodosRepository _todosRepository;

        public TodosController(ITodosRepository todosRepository)
        {
            _todosRepository = todosRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoResponse>> GetTodos()
        {
            var todos = await _todosRepository.GetTodosAsync();
            return todos.Select(todo => todo.MapToTodoResponse());
            //return todos.Select(todo => new TodoResponse
            //{
            //    Id = todo.Id,
            //    Title = todo.Title,
            //    Description = todo.Description,
            //    Difficulty = todo.Difficulty,
            //    IsDone = todo.IsDone,
            //    DateCreated = todo.DateCreated }
            //    });
        }
        //public IEnumerable<Todo> GetTodos()
        //{
        //    return _todosRepository.GetAll();
        //}

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

        //public ActionResult<Todo> GetComment(Guid todoId)
        //{
        //    var todo =_todosRepository.Get(todoId);

        //    if (todo == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(todo);
        //}

        [HttpPost]
         public async Task<ActionResult<TodoResponse>> AddTodo( AddTodoRequest request)
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

            return CreatedAtAction(nameof(Get), new {Id = todoItemReadModel.Id }, todoItemReadModel.MapToTodoResponse());
    }

        //public ActionResult<TodoResponse> AddTodo([FromBody] AddTodoRequest request)
        //{
        //    var todo = new Todo
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = request.Title,
        //        Description = request.Description,
        //        Difficulty = request.Difficulty,
        //        IsDone = request.IsDone,
        //        DateCreated = DateTime.Now
        //    };

    //    _todosRepository.Add(todo);

    //    return CreatedAtAction("GetTodo", new { todoId = todo.Id }, todo.MapToTodoResponse());
    //}

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<TodoResponse>> UpdateTodo(Guid id, UpdateTodoRequest request)
        {
            var todoItem = await _todosRepository.GetTodoAsync(id);
            if (todoItem is null)
            {
                return NotFound($"Todo item with specified id: '{id}' does not exist");
            }

            //var todoItemReadModel = new TodoReadModel
            //{
            //    Id = id,
            //    Title = request.Title,
            //    Description = request.Description, 
            //    IsDone = request.IsDone
            //};
           
            todoItem.Title = request.Title;
            todoItem.Description = request.Description;
            todoItem.IsDone = request.IsDone;

            await _todosRepository.SaveOrUpdateAsync(todoItem);

            return todoItem.MapToTodoResponse();
        }

        //public ActionResult<TodoResponse> UpdateTodo(Guid todoId, UpdateTodoRequest request)
        //{
        //    if (request is null)
        //    {
        //        return BadRequest();
        //    }

        //    var todoToUpdate = _todosRepository.Get(todoId);

        //    if (todoToUpdate is null)
        //    {
        //        return NotFound();
        //    }

        //    var updatedTodo = _todosRepository.Update(todoId, request.Title, request.Description, request.Difficulty, request.IsDone);

        //    return updatedTodo.MapToTodoResponse();
        //}

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            await _todosRepository.DeleteAsync(id);
            return NoContent();
        }

        //public IActionResult DeleteTodo(Guid todoId)
        //{
        //    var todoToDelete = _todosRepository.Get(todoId);

        //    if (todoToDelete is null)
        //    {
        //        return NotFound();
        //    }

        //    _todosRepository.Delete(todoId);

        //    return NoContent();
        //}
    }
}
