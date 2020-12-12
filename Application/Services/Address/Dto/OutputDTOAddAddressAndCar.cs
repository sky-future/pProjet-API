namespace Application.Services.Address.Dto
{
    public class OutputDTOAddAddressAndCar
    {

        public int Id { get; set; }
        
        public string Street { get; set; }
        
        public int Number { get; set; }
        
        public int PostalCode { get; set; }
        
        public string City { get; set; }
        
        public string Country { get; set; }
        
        public string Longitude { get; set; }
        
        public string Latitude { get; set; }  
        
        public string Immatriculation { get; set; }

        public int PlaceNb { get; set; }
    }
}