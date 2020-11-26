using System;

namespace Domain.Profile
{
    public class ProfileFactory : IProfileFactory
    {
        public IProfile CreateProfile(string lastname, string firstname, string matricule, string telephone, string descript, int idUser)
        {
        
            
            return new Profile
            {
                Lastname = lastname,
                Firstname = firstname,
                Matricule = matricule,
                Telephone =  telephone,
                Descript = descript,
                IdUser = idUser
                
            };
        }

    }
}