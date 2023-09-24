using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VozovyParkAutobazaru.Data;
using VozovyParkAutobazaru.Models;

namespace VozovyParkAutobazaru.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get Vehicles based on filter
        public async Task<IActionResult> FilterVehicles(string fuelType)
        {
            var vehicles = _context.Vehicles.AsQueryable();
            if (!string.IsNullOrEmpty(fuelType))
            {
                vehicles = vehicles.Where(v => v.FuelType == fuelType);
            }
            return Json(await vehicles.ToListAsync());
        }

        public async Task<IActionResult> Index() => View(await _context.Vehicles.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Manufacturer,Model,YearOfManufacture,OdometerReading,FuelType,BodyType,LicensePlate,Condition,InBazaarSince,Note")] VehicleViewModel vehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var vehicle = new Vehicle
                    {
                        Manufacturer = vehicleViewModel.Manufacturer,
                        Model = vehicleViewModel.Model,
                        YearOfManufacture = vehicleViewModel.YearOfManufacture,
                        OdometerReading = vehicleViewModel.OdometerReading,
                        FuelType = vehicleViewModel.FuelType,
                        BodyType = vehicleViewModel.BodyType,
                        LicensePlate = vehicleViewModel.LicensePlate,
                        Condition = vehicleViewModel.Condition,
                        InBazaarSince = vehicleViewModel.InBazaarSince,
                        Note = vehicleViewModel.Note
                    };

                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("LicensePlate", "SPZ již existuje, zadejte jiný údaj");
                    return Json(new { success = false, errorMessage = "SPZ již existuje, zadejte jiný údaj" });
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errorMessage = string.Join(", ", errors) });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturer,Model,YearOfManufacture,OdometerReading,FuelType,BodyType,LicensePlate,Condition,InBazaarSince,Note")] Vehicle vehicle)
        {
            if (id != vehicle.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> IsLicensePlateAvailable(string licensePlate)
        {
            var available = !(await _context.Vehicles.AnyAsync(e => e.LicensePlate == licensePlate));
            return Json(new { test = available });
        }

        public async Task<IActionResult> GetVehicles(string fuelType)
        {
            var vehicles = _context.Vehicles.AsQueryable();
            if (!string.IsNullOrEmpty(fuelType))
            {
                vehicles = vehicles.Where(v => v.FuelType == fuelType);
            }
            return Json(await vehicles.ToListAsync());
        }

        public IActionResult Statistics()
        {
            ViewBag.TotalVehicles = _context.Vehicles.Count();
            ViewBag.VehiclesUnder100k = _context.Vehicles.Count(v => v.OdometerReading >= 0 && v.OdometerReading <= 99999);
            ViewBag.VehiclesOver100k = _context.Vehicles.Count(v => v.OdometerReading > 100000);
            ViewBag.PetrolVehicles = _context.Vehicles.Count(v => v.FuelType == "Petrol");
            ViewBag.DieselVehicles = _context.Vehicles.Count(v => v.FuelType == "Diesel");
            ViewBag.AlternativeVehicles = _context.Vehicles.Count(v => v.FuelType != "Petrol" && v.FuelType != "Diesel");
            ViewBag.ExcellentConditionVehicles = _context.Vehicles.Count(v => v.Condition == "Excellent");
            ViewBag.GoodConditionVehicles = _context.Vehicles.Count(v => v.Condition == "Good");
            ViewBag.VeryWornConditionVehicles = _context.Vehicles.Count(v => v.Condition == "Very Worn");
            ViewBag.PoorConditionVehicles = _context.Vehicles.Count(v => v.Condition == "Poor");

            return View();
        }

        private bool VehicleExists(int id) => _context.Vehicles.Any(e => e.Id == id);
    }
}