using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Enums;
using RestAPI.Models;

namespace RestAPI.Persistence
{
    //public class TodosRepository : ITodosRepository
    //{
    //    private readonly List<Todo> _todos;

    //    public TodosRepository()
    //    {
    //        _todos = new List <Todo> ();
    //    }

    //    public void Add(Todo todo)
    //    {
    //        _todos.Add(todo);
    //    }

    //    public void Delete(Guid id)
    //    {
    //        var todoToDelete = _todos.FirstOrDefault(todo => todo.Id == id);
    //        _todos.Remove(todoToDelete);
    //    }

    //    public Todo Get(Guid id)
    //    {
    //        return _todos.FirstOrDefault(todo => todo.Id == id);
    //    }

    //    public IEnumerable<Todo> GetAll()
    //    {
    //        return _todos;
    //    }

    //    public Todo Update(Guid id, string title, string description, Difficulty difficulty, bool isDone)
    //    {
    //        var todo = _todos.First(todo => todo.Id == id);

    //        todo.Title = title;
    //        todo.Description = description;
    //        todo.Difficulty = difficulty;
    //        todo.IsDone = isDone;

    //        return todo;
    //    }
    //}
}
