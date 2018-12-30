using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Controllers
{
    public class CasesController : Controller
    {
        private readonly ICaseService _service;

        public CasesController(ICaseService service)
        {
            _service = service;
        }

        public IEnumerable<string> GetCaseModels()
        {
            return _service.GetCaseModels();
        }

        public async Task<Case> GetCaseByModel(string model)
        {
            return _service.GetCaseByModel(model);
        }

        // GET: Cases
        public async Task<IActionResult> Index()
        {
            return View(_service.GetAllCases());
        }

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = _service.GetCaseById(id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // GET: Cases/Create
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Create([Bind("CaseId,Model,Manufacturer,Price,Type")] Case @case)
        {
            if (ModelState.IsValid)
            {
                _service.InsertCase(@case);
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }

        // GET: Cases/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = _service.GetCaseById(id);
            if (@case == null)
            {
                return NotFound();
            }
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int id, [Bind("CaseId,Model,Manufacturer,Price,Type")] Case @case)
        {
            if (id != @case.CaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(@case);
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }

        // GET: Cases/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = _service.GetCaseById(id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: Cases/Delete/5
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
