using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Data;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Controllers
{
    public class MotherboardsController : Controller
    {
        private readonly IMotherboardService _service;

        public MotherboardsController(IMotherboardService service)
        {
            _service = service;
        }

        // GET: Motherboards
        public async Task<IActionResult> Index()
        {
            return View(_service.GetAllMbs());
        }

        public List<string> GetMotherboardModels()
        {
            return _service.GetMbModels().ToList();
        }

        public Motherboard GetMotherboardByModel(string model)
        {
            var mb = _service.GetMbByModel(model);
            return mb;
        }

        // GET: Motherboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = _service.GetMbById(id);
            if (motherboard == null)
            {
                return NotFound();
            }

            return View(motherboard);
        }

        // GET: Motherboards/Create
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Motherboards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Create([Bind("MotherboardId,Model,Manufacturer,Price,CpuSocket")] Motherboard motherboard)
        {
            if (ModelState.IsValid)
            {
                _service.InsertMb(motherboard);
                return RedirectToAction(nameof(Index));
            }
            return View(motherboard);
        }

        // GET: Motherboards/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = _service.GetMbById(id);
            if (motherboard == null)
            {
                return NotFound();
            }
            return View(motherboard);
        }

        // POST: Motherboards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int id, [Bind("MotherboardId,Model,Manufacturer,Price,CpuSocket")] Motherboard motherboard)
        {
            if (id != motherboard.MotherboardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(motherboard);
                return RedirectToAction(nameof(Index));
            }
            return View(motherboard);
        }

        // GET: Motherboards/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = _service.GetMbById(id);
            if (motherboard == null)
            {
                return NotFound();
            }

            return View(motherboard);
        }

        // POST: Motherboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
