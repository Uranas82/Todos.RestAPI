using Contracts.Enums;
using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface ITodosRepository
    {  
        Task<IEnumerable<TodoReadModel>> GetTodosAsync();

        Task<TodoReadModel> GetTodoAsync(Guid id);

        //Task<int> SaveAsync(TodoWriteModel model);

        //Task<int> UpdateAsync(Guid id, string title, string description, Difficulty difficulty, bool isDone);

        Task<int> DeleteAsync(Guid id);

        Task<int> SaveOrUpdateAsync(TodoReadModel model);



        //Task<IEnumerable<RecipeReadModel>> GetAll(RecipesFilter filter);

        //Get();

        //Add();

        //Update();

        //Delete();



        //Task<> GetAll();

        //Task<int> SaveAsync(RecipeWriteModel model);

        //Task<int> EditNameAsync(int id, string name);

        //Task<int> DeleteByIdAsync(int id);

        //Task<int> DeleteAllAsync();
    }
}
