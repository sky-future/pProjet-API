using System.Collections.Generic;
using Domain.Users;

namespace Application.Services.AddressUser.Dto
{
    public class OutputDtoGetByIdAddressAddressUser
    {
        public IEnumerable<IUser> Users { get; set; }
    }
}