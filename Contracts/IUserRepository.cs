﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string userId, bool trackChanges);
        void UpdateUser(User user);

    }
}
