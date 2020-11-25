namespace Domain.Profile
{
    public interface IProfileFactory
    {
        IProfile CreateProfile(string name, string firstname, string matricule, string telephone, string description);
        IProfile CreateProfileWithId(int id, string lastname, string firstname, string matricule, string telephone, string descript);
    }
}