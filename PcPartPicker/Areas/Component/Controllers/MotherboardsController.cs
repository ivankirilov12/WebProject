﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Data;
using PcPartPicker.Definitions;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Areas.Component
{
    [Area("Component")]
    public class MotherboardsController : Controller
    {
        private readonly IMotherboardService _service;
        private readonly IGoogleDriveService _driveService;

        public MotherboardsController(IMotherboardService service, IGoogleDriveService driveService)
        {
            _service = service;
            _driveService = driveService;
        }

        // GET: Motherboards
        public IActionResult Index()
        {
            return View(_service.GetAllMbs());
        }

        public IEnumerable<Motherboard> GetMotherboards(int? skip, int? take)
        {
            return _service.GetAllMbs(skip, take);
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
        public IActionResult Details(int? id)
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
        public IActionResult Create([Bind("MotherboardId,Model,Manufacturer,Price,CpuSocket")] Motherboard motherboard)
        {
            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    motherboard.ImgUrl = _driveService.UploadFile(image);
                }
                else
                {
                    motherboard.ImgUrl = Constants.DEFAULT_MB_IMG;
                }
                _service.InsertMb(motherboard);
                return RedirectToAction(nameof(Index));
            }
            return View(motherboard);
        }

        // GET: Motherboards/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int? id)
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
        public IActionResult Edit(int id, [Bind("MotherboardId,Model,Manufacturer,Price,CpuSocket")] Motherboard motherboard)
        {
            if (id != motherboard.MotherboardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    _driveService.DeleteFile(Request.Form["ImgUrl"]);
                    motherboard.ImgUrl = _driveService.UploadFile(image);
                }
                _service.Update(motherboard);
                return RedirectToAction(nameof(Index));
            }
            return View(motherboard);
        }

        // GET: Motherboards/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Delete(int? id)
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
        public IActionResult DeleteConfirmed(int id)
        {
            var mb = _service.GetMbById(id);
            string imgUrl = mb.ImgUrl;
            if (imgUrl != Constants.DEFAULT_MB_IMG)
            {
                _driveService.DeleteFile(imgUrl);
            }

            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
