﻿using System.Collections.Generic;
using Domain.Users;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Query();
        IUser GetById(int id);
        IUser Create(IUser user);
        bool DeleteById(int id);
        bool Update(int id, IUser user);
        IUser Authenticate(string mail, string password);
    }
}