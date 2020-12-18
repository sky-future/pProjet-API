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
            var queryInDb = _userRepository.Query();

            if (queryInDb == null)
            {
                return null;
            }
            
            return queryInDb.Select(user => new OutputDtoQueryUser
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
            var userFromDto = _userFactory.CreateSimpleUser(inputDtoUpdateUser.mail, inputDtoUpdateUser.password,
                inputDtoUpdateUser.lastConnexion);

            return _userRepository.Update(id, userFromDto);
        }

        public OutputDtoGetByIdUser GetById(InputDtoGetByIdUser inputDtoGetById)
        {
            var userInDb = _userRepository.GetById(inputDtoGetById.id);
            
            if (userInDb == null)
            {
                return null;
            }
            
            return new OutputDtoGetByIdUser
            {
                id = userInDb.Id,
                mail = userInDb.Mail,
                password = userInDb.Password,
                lastConnexion = userInDb.LastConnexion,
                admin = userInDb.Admin
            };
        }

        public bool DeleteById(InputDtoDeleteByIdUser inputDtoDeleteById)
        {
            return _userRepository.DeleteById(inputDtoDeleteById.id);
        }

        public OutputDtoAuthenticate Authenticate(InputDtoAuthenticate inputDtoAuthenticate)
        {
            var userInDb = _userRepository.Authenticate(inputDtoAuthenticate.mail, inputDtoAuthenticate.password);

            if (userInDb == null)
            {
                return null;
            }
            
            return new OutputDtoAuthenticate
            {
                id = userInDb.Id,
                mail = userInDb.Mail,
                password = userInDb.Password,
                lastConnexion = userInDb.LastConnexion,
                admin = userInDb.Admin,
                token = userInDb.Token,
                profile = 0,
            };
        }

        public bool UpdatePassword( InputDTOUpdateUserPassword inputDTOupdatePassword)
        {
            // var id = inputDTOupdatePassword.IdUser;
            // var newPassword = inputDTOupdatePassword.PasswordNew;
            // var oldPassword = inputDTOupdatePassword.PasswordOld;

            return _userRepository.UpdatePassword(inputDTOupdatePassword);
        }

        public bool UpdateLastConnexion(InputDtoUpdateLastConnexion lastConnexion)
        {
            return _userRepository.UpdateLastConnexion(lastConnexion);
        }

        public bool CreateAdminUser(InputDtoAddAdminUser inputDtoAddAdminUser)
        {
            var userFromDto = _userFactory.CreateAdminUser(inputDtoAddAdminUser.Mail, inputDtoAddAdminUser.Password,
                inputDtoAddAdminUser.LastConnexion, inputDtoAddAdminUser.Admin);

            return _userRepository.CreateAdminUser(userFromDto);
        }
    }
}