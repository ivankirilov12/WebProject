using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcPartPicker.Definitions;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Areas.Component
{
    [Area("Component")]
    public class CpusController : Controller
    {
        private readonly ICpuService _service;
        private readonly IGoogleDriveService _driveService;

        public CpusController(ICpuService service, IGoogleDriveService driveService)
        {
            _service = service;
            _driveService = driveService;
        }

        // GET: Cpus
        public IActionResult Index()
        {
            return View(_service.GetAllCpus());
        }
               
        public List<string> GetCpuModels()
        {
            return _service.GetCpuModels().ToList();
        }

        public IEnumerable<Cpu> GetCpus(int? skip, int? take)
        {
            return _service.GetAllCpus(skip, take);
        }

        public Cpu GetCpuByModel(string model)
        {
            return _service.GetCpuByModel(model);
        }


        // GET: Cpus/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpu = _service.GetCpuById(id);
            if (cpu == null)
            {
                return NotFound();
            }

            return View(cpu);
        }

        // GET: Cpus/Create
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cpus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create([Bind("CpuId,Model,Price,Manufacturer,Socket,NumberOfCores,CacheMemory")] Cpu cpu)
        {
            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    cpu.ImgUrl = _driveService.UploadFile(image);
                }
                else
                {
                    cpu.ImgUrl = Constants.DEFAULT_CPU_IMG;
                }

                _service.InsertCpu(cpu);
                return RedirectToAction(nameof(Index));
            }
            return View(cpu);
        }

        // GET: Cpus/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpu = _service.GetCpuById(id);
            if (cpu == null)
            {
                return NotFound();
            }
            return View(cpu);
        }

        // POST: Cpus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int id, [Bind("CpuId,Model,Price,Manufacturer,Socket,NumberOfCores,CacheMemory")] Cpu cpu)
        {
            if (id != cpu.CpuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _driveService.DeleteFile(Request.Form["ImgUrl"]);
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    cpu.ImgUrl = _driveService.UploadFile(image);
                }
                _service.Update(cpu);
                return RedirectToAction(nameof(Index));
            }
            return View(cpu);
        }

        // GET: Cpus/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpu = _service.GetCpuById(id);
            if (cpu == null)
            {
                return NotFound();
            }

            return View(cpu);
        }

        // POST: Cpus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cpu = _service.GetCpuById(id);
            var cpuImage = cpu.ImgUrl;
            if (cpuImage != Constants.DEFAULT_CPU_IMG)
            {
                _driveService.DeleteFile(cpu.ImgUrl);
            }            

            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }   
}
