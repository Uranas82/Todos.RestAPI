using Persistence.Models.ReadModels;
using RestAPI.Models;
using RestAPI.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI
{
    public static class Extensions
    {
        public static TodoResponse MapToTodoResponse(this TodoReadModel todo)
        {
            return new TodoResponse
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Difficulty = todo.Difficulty,
                IsDone = todo.IsDone,
                DateCreated = todo.DateCreated
            };
        }
    }
}
