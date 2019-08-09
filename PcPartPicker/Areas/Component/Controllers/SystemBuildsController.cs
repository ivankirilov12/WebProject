using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Data;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Areas.Component
{
    [Area("Component")]
    public class SystemBuildsController : Controller
    {
        private readonly ISystemBuildService _service;

        public SystemBuildsController(ISystemBuildService service)
        {
            _service = service;
        }

        // GET: SystemBuilds
        public IActionResult Index()
        {
            return View(_service.GetAllSystemBuilds());
        }

        public IEnumerable<SystemBuild> GetSystemBuilds(int? skip, int? take)
        {
            return _service.GetAllSystemBuilds(skip, take);
        }

        // GET: SystemBuilds/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = _service.GetSystemBuildById(id);
            if (systemBuild == null)
            {
                return NotFound();
            }

            return View(systemBuild);
        }
        
        // GET: SystemBuilds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemBuilds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create(IFormCollection collection)
        {
            string cpuModel = Request.Form["cpus"].ToString();
            string caseModel = Request.Form["cases"].ToString();
            string gpuModel = Request.Form["gpus"].ToString();
            string memoryOptionModel = Request.Form["memoryoptions"].ToString();
            string motherboardModel = Request.Form["motherboards"].ToString();
            string storageOptionModel = Request.Form["storageoptions"].ToString();

            decimal price = decimal.Parse(Request.Form["Price"].ToString());
            string name = Request.Form["Name"].ToString();
            string description = Request.Form["Description"].ToString();
            _service.InsertSystemBuild(cpuModel, caseModel, gpuModel, memoryOptionModel, motherboardModel, storageOptionModel, price, name, description);
            if (ModelState.IsValid) // remove?
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: SystemBuilds/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = _service.GetSystemBuildById(id);
            if (systemBuild == null)
            {
                return NotFound();
            }
            return View(systemBuild);
        }

        // POST: SystemBuilds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int id, [Bind("SystemBuildId,Price")] SystemBuild systemBuild)
        {
            if (id != systemBuild.SystemBuildId)
            {
                return NotFound();
            }

            string cpuModel = Request.Form["cpus"].ToString();
            string caseModel = Request.Form["cases"].ToString();
            string gpuModel = Request.Form["gpus"].ToString();
            string memoryOptionModel = Request.Form["memoryoption"].ToString();
            string motherboardModel = Request.Form["motherboards"].ToString();
            string storageOptionModel = Request.Form["storageoption"].ToString();

            string name = Request.Form["Name"].ToString();
            string description = Request.Form["Description"].ToString();

            if (ModelState.IsValid)
            {
                _service.Update(cpuModel, caseModel, gpuModel, memoryOptionModel, motherboardModel, storageOptionModel, name, description, id);
                return RedirectToAction(nameof(Index));
            }
            return View(systemBuild);
        }

        // GET: SystemBuilds/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = _service.GetSystemBuildById(id);
            if (systemBuild == null)
            {
                return NotFound();
            }

            return View(systemBuild);
        }

        // POST: SystemBuilds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
