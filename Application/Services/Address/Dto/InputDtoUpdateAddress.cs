namespace Application.Services.Address.Dto
{
    public class InputDtoUpdateAddress
    {
        public string street { get; set; }
        public int number { get; set; }
        public int postalCode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        
    }
}