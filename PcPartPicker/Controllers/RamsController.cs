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
    public class RamsController : Controller
    {
        private readonly IMemoryOptionService _service;

        public RamsController(IMemoryOptionService service)
        {
            _service = service;
        }

        // GET: Rams
        public async Task<IActionResult> Index()
        {
            return View(_service.GetAllMemoryOptions());
        }

        public List<string> GetRamModels()
        {
            return _service.GetMemoryOptionModels().ToList();
        }

        public Ram GetRamByModel(string model)
        {
            return _service.GetMemoryOptionByModel(model);
        }

        // GET: Rams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = _service.GetMemoryOptionById(id);
            if (ram == null)
            {
                return NotFound();
            }

            return View(ram);
        }

        // GET: Rams/Create
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Create([Bind("RamId,Model,Manufacturer,MemoryType,MemoryCapacity,MemoryFrequency,Price")] Ram ram)
        {
            if (ModelState.IsValid)
            {
                _service.InsertMemoryOption(ram);
                return RedirectToAction(nameof(Index));
            }
            return View(ram);
        }

        // GET: Rams/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = _service.GetMemoryOptionById(id);
            if (ram == null)
            {
                return NotFound();
            }
            return View(ram);
        }

        // POST: Rams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int id, [Bind("RamId,Model,Manufacturer,MemoryType,MemoryCapacity,MemoryFrequency,Price")] Ram ram)
        {
            if (id != ram.RamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(ram);
                return RedirectToAction(nameof(Index));
            }
            return View(ram);
        }

        // GET: Rams/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = _service.GetMemoryOptionById(id);
            if (ram == null)
            {
                return NotFound();
            }

            return View(ram);
        }

        // POST: Rams/Delete/5
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
