﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcPartPicker.Definitions;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Areas.Component
{
    [Area("Component")]
    public class GpusController : Controller
    {
        private readonly IGpuService _service;
        private readonly IGoogleDriveService _driveService;

        public GpusController(IGpuService service, IGoogleDriveService driveService)
        {
            _service = service;
            _driveService = driveService;
        }

        // GET: Gpus
        public IActionResult Index()
        {
            return View(_service.GetAllGpus());
        }

        public IEnumerable<Gpu> GetGpus(int? skip, int? take)
        {
            return _service.GetAllGpus(skip, take);
        }

        public List<string> GetGpuModels()
        {
            return _service.GetGpuModels().ToList();
        }

        public Gpu GetGpuByModel(string model)
        {
            return _service.GetGpuByModel(model);
        }

        // GET: Gpus/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpu = _service.GetGpuById(id);
            if (gpu == null)
            {
                return NotFound();
            }

            return View(gpu);
        }

        // GET: Gpus/Create
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gpus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Create([Bind("Model,Manufacturer,Price,Memory")] Gpu gpu)
        {
            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    gpu.ImgUrl = _driveService.UploadFile(image);
                }
                else
                {
                    gpu.ImgUrl = Constants.DEFAULT_GPU_IMG;
                }
                _service.InsertGpu(gpu);
                return RedirectToAction(nameof(Index));
            }
            return View(gpu);
        }

        // GET: Gpus/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpu = _service.GetGpuById(id);
            if (gpu == null)
            {
                return NotFound();
            }
            return View(gpu);
        }

        // POST: Gpus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int? id, [Bind("GpuId,Model,Manufacturer,Price,Memory")] Gpu gpu)
        {
            if (id != gpu.GpuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    _driveService.DeleteFile(Request.Form["ImgUrl"]);
                    gpu.ImgUrl = _driveService.UploadFile(image);
                }
                _service.Update(gpu);
                
                return RedirectToAction(nameof(Index));
            }
            return View(gpu);
        }

        // GET: Gpus/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpu = _service.GetGpuById(id);
            if (gpu == null)
            {
                return NotFound();
            }

            return View(gpu);
        }

        // POST: Gpus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult DeleteConfirmed(int id)
        {
            var gpu = _service.GetGpuById(id);
            string imgUrl = gpu.ImgUrl;
            if (imgUrl != Constants.DEFAULT_GPU_IMG)
            {
                _driveService.DeleteFile(gpu.ImgUrl);
            }

            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
