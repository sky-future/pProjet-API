namespace Application.Services.Profile.DTO
{
    public class OutputDtoQueryProfile
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Matricule { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        
        public int IdUser { get; set; }
    }
}