using Domain.Shared;

namespace Domain.Profile
{
    public interface IProfile : IEntity
    {
        string Lastname { get; set; }
        string Firstname { get; set; }
        string Matricule { get; set; }
        string Telephone { get; set; }
        string Descript { get; set; }
        int IdUser { get; set; }
    }
}