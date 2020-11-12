using System;
using Domain.Shared;

namespace Domain.Users
{
    public interface IUser : IEntity
    {
        string Mail { get; set; }
        string Password { get; set; }
        string LastConnexion { get; set; }
        bool Admin { get; set; }
        string Token { get; set; }
    }
}