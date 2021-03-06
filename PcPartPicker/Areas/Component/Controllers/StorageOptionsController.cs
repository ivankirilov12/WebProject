﻿using System.Collections.Generic;
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
    public class StorageOptionsController : Controller
    {
        private readonly IStorageOptionService _service;
        private readonly IGoogleDriveService _driveService;

        public StorageOptionsController(IStorageOptionService service, IGoogleDriveService driveService)
        {
            _service = service;
            _driveService = driveService;
        }

        // GET: Storages
        public IActionResult Index()
        {
            return View(_service.GetAllStorageOptions());
        }

        public IEnumerable<StorageOption> GetStorageOptions(int? skip, int? take)
        {
            return _service.GetAllStorageOptions(skip, take);
        }

        public List<string> GetStorageOptionModels()
        {
            return _service.GetStorageOptionModels().ToList();
        }

        public StorageOption GetStorageOptionByModel(string model)
        {
            return _service.GetStorageOptionByModel(model);
        }

        // GET: Storages/Details/5
        public IActionResult Details(int? id)
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
        public IActionResult Create([Bind("StorageId,Model,Manufacturer,Price,Type,Capacity")] StorageOption storageOption)
        {
            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    storageOption.ImgUrl = _driveService.UploadFile(image);
                }
                else
                {
                    storageOption.ImgUrl = Constants.DEFAULT_STORAGE_IMG;
                }
                _service.InsertStorageOption(storageOption);
                return RedirectToAction(nameof(Index));
            }
            return View(storageOption);
        }

        // GET: Storages/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int? id)
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
        public IActionResult Edit(int id, [Bind("StorageId,Model,Manufacturer,Price,Type,Capacity")] StorageOption storageOption)
        {
            if (id != storageOption.StorageOptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    _driveService.DeleteFile(Request.Form["ImgUrl"]);
                    storageOption.ImgUrl = _driveService.UploadFile(image);
                }
                _service.Update(storageOption);
                return RedirectToAction(nameof(Index));
            }
            return View(storageOption);
        }

        // GET: Storages/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Delete(int? id)
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
        public IActionResult DeleteConfirmed(int id)
        {
            var storage = _service.GetStorageOptionById(id);
            string imgUrl = storage.ImgUrl;
            if (imgUrl != Constants.DEFAULT_STORAGE_IMG)
            {
                _driveService.DeleteFile(imgUrl);
            }            

            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
