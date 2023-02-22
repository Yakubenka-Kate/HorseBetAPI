using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<User> GetUserAsync(string userId, bool trackChanges)
            => await FindByCondition(u => u.Id.Equals(userId), trackChanges).SingleOrDefaultAsync();

        public void UpdateUser(User user) => Update(user);

    }
}
