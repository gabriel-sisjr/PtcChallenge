using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace PtcChallenge.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService) => _ownerService = ownerService;

        public async Task<IActionResult> Index() => View(await _ownerService.Get());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] OwnerModel owner)
        {
            if (await _ownerService.InsertAsync(owner))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }

        [HttpGet("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            if (await _ownerService.ChangeStatusAsync(id))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id) => View(await _ownerService.GetByIdAsync(id));

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] OwnerModel owner)
        {
            if (await _ownerService.UpdateAsync(owner))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { msg = "Error" });
        }
    }
}
