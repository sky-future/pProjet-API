namespace Application.Services.RequestCarpooling.DTO
{
    public class OutputDTORequestCarpoolingByID
    {
        public int IdUser { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Telephone { get; set; }
        public bool Confirmation { get; set; }
    }
}