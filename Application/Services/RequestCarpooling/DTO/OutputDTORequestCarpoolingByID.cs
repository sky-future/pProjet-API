using System.Collections.Generic;

namespace Application.Services.RequestCarpooling.DTO
{
    public class OutputDtoRequestCarpoolingById
    {
        public int IdUser { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Telephone { get; set; }
        public int Confirmation { get; set; }
    }
}