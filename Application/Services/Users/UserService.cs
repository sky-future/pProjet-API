using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Users.Dto;
using Domain.Users;

namespace Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory = new UserFactory();

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public IEnumerable<OutputDtoQueryUser> Query()
        {
            return _userRepository
                .Query()
                .Select(user => new OutputDtoQueryUser
            {
                id =  user.Id,
                mail = user.Mail,
                password = user.Password,
                lastConnexion = user.LastConnexion,
                admin = user.Admin
            });
        }

        public OutputDtoAddUser Create(InputDtoAddUser inputDtoAddUser)
        {
            var userFromDto = _userFactory.CreateSimpleUser(inputDtoAddUser.mail, inputDtoAddUser.password,
                inputDtoAddUser.lastConnexion);

            var userInDb = _userRepository.Create(userFromDto);
            
            return new OutputDtoAddUser
            {
                id = userInDb.Id,
                mail = userInDb.Mail,
                password = userInDb.Password,
                lastConnexion = userInDb.LastConnexion,
                admin = userInDb.Admin
            };

        }

        public bool Update(int id, InputDtoUpdateUser inputDtoUpdateUser)
        {
            var userFromDto = _userFactory.CreateAdminUser(inputDtoUpdateUser.mail, inputDtoUpdateUser.password,
                inputDtoUpdateUser.lastConnexion, inputDtoUpdateUser.admin);

            return _userRepository.Update(id, userFromDto);
        }
    }
}