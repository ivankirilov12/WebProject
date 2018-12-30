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
    public class StoragesController : Controller
    {
        private readonly IStorageOptionService _service;

        public StoragesController(IStorageOptionService service)
        {
            _service = service;
        }

        // GET: Storages
        public async Task<IActionResult> Index()
        {
            return View(_service.GetAllStorageOptions());
        }

        public List<string> GetStorageModels()
        {
            return _service.GetStorageOptionModels().ToList();
        }

        public Storage GetStorageByModel(string model)
        {
            return _service.GetStorageOptionByModel(model);
        }

        // GET: Storages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = _service.GetStorageOptionById(id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Storages/Create
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Storages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Create([Bind("StorageId,Model,Manufacturer,Price,Type,Capacity")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                _service.InsertStorageOption(storage);
                return RedirectToAction(nameof(Index));
            }
            return View(storage);
        }

        // GET: Storages/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = _service.GetStorageOptionById(id);
            if (storage == null)
            {
                return NotFound();
            }
            return View(storage);
        }

        // POST: Storages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int id, [Bind("StorageId,Model,Manufacturer,Price,Type,Capacity")] Storage storage)
        {
            if (id != storage.StorageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(storage);
                return RedirectToAction(nameof(Index));
            }
            return View(storage);
        }

        // GET: Storages/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = _service.GetStorageOptionById(id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // POST: Storages/Delete/5
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
