using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace PtcChallenge.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService) => _brandService = brandService;
        public async Task<IActionResult> Index() => View(await _brandService.Get());
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BrandModel brand)
        {
            if (await _brandService.InsertAsync(brand))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }

        [HttpGet("[controller]/ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            if (await _brandService.ChangeStatusAsync(id))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }
    }
}
