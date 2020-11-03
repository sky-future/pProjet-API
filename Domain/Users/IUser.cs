using System;
using Domain.Shared;

namespace Domain.Users
{
    public interface IUser : IEntity
    {
        string Mail { get; set; }
        string Password { get; set; }
        DateTime LastConnexion { get; set; }
        bool Admin { get; set; }
        
    }
}