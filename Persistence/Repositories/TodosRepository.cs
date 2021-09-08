using Persistence.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    class TodosRepository : ITodosRepository
    {
        private const string TodosTable = "todos";
        private readonly ISqlClient _sqlClient;

        public TodosRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<TodoReadModel>> GetTodosAsync()
        {
            var sql = $"SELECT * FROM {TodosTable}";

            return _sqlClient.QueryAsync<TodoReadModel>(sql);
        }

        public Task<TodoReadModel> GetTodoAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TodosTable} WHERE Id = @Id";

            return _sqlClient.QuerySingleOrDefaultAsync<TodoReadModel>(sql, new {Id = id});
        }

        public Task<int> SaveOrUpdateAsync(TodoReadModel model)
        {
            var sql = @$"INSERT INTO {TodosTable} (Id, Title, Description, Difficulty, DateCreated, IsDone)
            VALUES(@Id, @Title, @Description, @Difficulty, @DateCreated, @IsDone)
            ON DUPLICATE KEY UPDATE Title = @Title, Description = @Description, Difficulty = @Difficulty, IsDone = @IsDone";

            return _sqlClient.ExecuteAsync(sql, new
            {
                model.Id,
                model.Title,
                model.Description,
                Difficulty = model.Difficulty.ToString(),
                model.IsDone,
                model.DateCreated
            });
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var sql = @$"DELETE FROM {TodosTable} WHERE Id = @Id";

            return _sqlClient.ExecuteAsync(sql, new
            {
                Id = id
            });
        }



        //public Task<int> SaveAsync(TodoWriteModel model)
        //{
        //    var sql = @$"INSERT INTO {TodosTable} (Id, Title, Description, Difficulty, IsDone, DateCreated)
        //                VALUES (@Id, @Title, @Description, @Difficulty, @IsDone, @DateCreated)";

        //    return _sqlClient.ExecuteAsync(sql, model);
        //}

        //public Task<int> UpdateAsync(Guid id, string title, string description, Difficulty difficulty, bool isDone)
        //{
        //    var sql = @$"UPDATE {TodosTable} SET 
        //                Title = @Title,
        //                Description = @Description,
        //                Difficulty = @Difficulty, 
        //                IsDone = @IsDone
        //                WHERE Id = @Id";

        //    return _sqlClient.ExecuteAsync(sql, new
        //    {
        //        Title = title,
        //        Description = description,
        //        Difficulty = difficulty,
        //        IsDone = isDone,
        //        Id = id
        //    });
    }
    //vietoj add ideti SaveOrUpdate
    // var sql = @$"INSERT INTO {TableName} (Id, Title, Description, Difficulty, DateCreated, IsDone) VALUES(@Id, @Title, @Description, @Difficulty, @DateCreated, @IsDone)
    // ON DUPLICATE KEY UPDATE Title = @Title, Description = @Description, Difficulty = @Difficulty, IsDone = @IsDone";

}

