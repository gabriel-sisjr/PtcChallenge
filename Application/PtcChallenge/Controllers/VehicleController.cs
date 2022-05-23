using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace PtcChallenge.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IOwnerService _ownerService;
        private readonly IBrandService _brandService;
        public VehicleController(IVehicleService vehicleService, IOwnerService ownerService, IBrandService brandService)
        {
            _vehicleService = vehicleService;
            _ownerService = ownerService;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index() => View(await _vehicleService.Get());

        public async Task<IActionResult> Create()
        {
            var availableBrands = await _brandService.GetAllBrandsAvailable();
            ViewBag.AvailableBrands = availableBrands;

            var availableOwners = await _ownerService.GetAllOwnersAvailable();
            ViewBag.AvailableOwners = availableOwners;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] VehicleModel vehicle)
        {
            if (await _vehicleService.InsertAsync(vehicle))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }

        [HttpGet("[controller]/SellVehicle/{id}")]
        public async Task<IActionResult> SellVehicle(int id)
        {
            if (await _vehicleService.ChangeStatusAsync(id, StatusVehicle.SELLED))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }

        [HttpGet("[controller]/InvalidateVehicle/{id}")]
        public async Task<IActionResult> InvalidateVehicle(int id)
        {
            if (await _vehicleService.ChangeStatusAsync(id, StatusVehicle.UNAVAILABLE))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }

        [HttpGet("[controller]/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var availableBrands = await _brandService.GetAllBrandsAvailable();
            ViewBag.AvailableBrands = availableBrands;

            var availableOwners = await _ownerService.GetAllOwnersAvailable();
            ViewBag.AvailableOwners = availableOwners;
            return View(await _vehicleService.GetByIdAsync(id));
        }

        [HttpPost("[controller]/Edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] VehicleModel vehicle)
        {
            if (await _vehicleService.UpdateAsync(vehicle))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }
    }
}
