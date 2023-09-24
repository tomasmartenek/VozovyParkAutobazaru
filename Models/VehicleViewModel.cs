using System.ComponentModel.DataAnnotations;

namespace VozovyParkAutobazaru.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vyplňte výrobce vozidla.")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Vyplňte model vozidla.")]
        public string Model { get; set; }

        [Range(1900, 2100, ErrorMessage = "Rok výroby musí být mezi 1900 a 2100.")]
        public int YearOfManufacture { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stav tachometru musí být kladné číslo.")]
        public int OdometerReading { get; set; }

        [Required(ErrorMessage = "Vyberte typ paliva.")]
        public string FuelType { get; set; }

        [Required(ErrorMessage = "Vyberte typ karoserie.")]
        public string BodyType { get; set; }

        [Required(ErrorMessage = "Vyplňte SPZ vozidla.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "SPZ musí mít délku mezi 5 a 10 znaky.")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Vyberte stav vozidla.")]
        public string Condition { get; set; }

        [Required(ErrorMessage = "Vyplňte datum, kdy bylo vozidlo přidáno do bazaru.")]
        public DateTime InBazaarSince { get; set; }

        [StringLength(500, ErrorMessage = "Poznámka může mít maximálně 500 znaků.")]
        public string Note { get; set; }
    }
}