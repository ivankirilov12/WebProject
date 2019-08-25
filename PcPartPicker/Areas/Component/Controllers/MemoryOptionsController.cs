using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcPartPicker.Definitions;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Areas.Component
{
    [Area("Component")]
    public class MemoryOptionsController : Controller
    {
        private readonly IMemoryOptionService _service;
        private readonly IGoogleDriveService _driveService;

        public MemoryOptionsController(IMemoryOptionService service, IGoogleDriveService driveService)
        {
            _service = service;
            _driveService = driveService;
        }

        // GET: Rams
        public IActionResult Index()
        {
            return View(_service.GetAllMemoryOptions());
        }

        public IEnumerable<MemoryOption> GetMemoryOptions(int? skip, int? take)
        {
            return _service.GetAllMemoryOptions(skip, take);
        }

        public List<string> GetMemoryOptionModels()
        {
            return _service.GetMemoryOptionModels().ToList();
        }

        public MemoryOption GetMemoryOptionByModel(string model)
        {
            return _service.GetMemoryOptionByModel(model);
        }

        // GET: Rams/Details/5
        public IActionResult Details(int? id)
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
        public IActionResult Create([Bind("RamId,Model,Manufacturer,MemoryType,MemoryCapacity,MemoryFrequency,Price")] MemoryOption memoryOption)
        {
            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    memoryOption.ImgUrl = _driveService.UploadFile(image);
                }
                else
                {
                    memoryOption.ImgUrl = Constants.DEFAULT_MEMORY_IMG;
                }
                _service.InsertMemoryOption(memoryOption);
                return RedirectToAction(nameof(Index));
            }
            return View(memoryOption);
        }

        // GET: Rams/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int? id)
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
        public IActionResult Edit(int id, [Bind("RamId,Model,Manufacturer,MemoryType,MemoryCapacity,MemoryFrequency,Price")] MemoryOption memoryOption)
        {
            if (id != memoryOption.MemoryOptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {                
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    _driveService.DeleteFile(Request.Form["ImgUrl"]);
                    memoryOption.ImgUrl = _driveService.UploadFile(image);
                }
                _service.Update(memoryOption);
                return RedirectToAction(nameof(Index));
            }
            return View(memoryOption);
        }

        // GET: Rams/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Delete(int? id)
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
        public IActionResult DeleteConfirmed(int id)
        {
            var memoryOption = _service.GetMemoryOptionById(id);
            string imgUrl = memoryOption.ImgUrl;
            if (imgUrl != Constants.DEFAULT_MEMORY_IMG)
            {
                _driveService.DeleteFile(memoryOption.ImgUrl);
            }            

            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
