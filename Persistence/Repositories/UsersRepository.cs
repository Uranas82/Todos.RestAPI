using Persistence.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly Dictionary<string, ApiKeyReadModel> _apiKeys;

        public UsersRepository()
        {
            _apiKeys = new Dictionary<string, ApiKeyReadModel>
            {
                { "11111", new ApiKeyReadModel
                {
                    Id = Guid.NewGuid(),
                    Key = "11111",
                    UserId = Guid.NewGuid(),
                    IsActive = true,
                    DateCreated = DateTime.Now
                }
                }
            };
        }

        ApiKeyReadModel IUsersRepository.GetApiKey(string key)
        {
            var result = _apiKeys.TryGetValue(key, out var apiKey);

            return apiKey;
        }
    }
}
