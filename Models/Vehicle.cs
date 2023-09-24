using System.ComponentModel.DataAnnotations;

namespace VozovyParkAutobazaru.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(1886, 9999)]
        public int YearOfManufacture { get; set; }

        [Required]
        public int OdometerReading { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public string BodyType { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public DateTime InBazaarSince { get; set; }

        public string Note { get; set; }
    }
}