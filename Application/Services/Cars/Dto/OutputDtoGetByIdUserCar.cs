namespace Application.Services.Cars.Dto
{
    public class OutputDtoGetByIdUserCar
    {
        public int Id { get; set; }
        public string Immatriculation { get; set; }
        public int IdUser { get; set; }
        public int PlaceNb { get; set; }
    }
}