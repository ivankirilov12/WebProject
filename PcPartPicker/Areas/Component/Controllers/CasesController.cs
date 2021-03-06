﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcPartPicker.Definitions;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Areas.Component
{
    [Area("Component")]
    public class CasesController : Controller
    {
        private readonly ICaseService _service;
        private readonly IGoogleDriveService _driveService;

        public CasesController(ICaseService service, IGoogleDriveService driveService)
        {
            _service = service;
            _driveService = driveService;
        }

        public IEnumerable<string> GetCaseModels()
        {
            return _service.GetCaseModels();
        }

        public IEnumerable<Case> GetCases(int? skip, int? take)
        {
            return _service.GetAllCases(skip, take);
        }

        public Case GetCaseByModel(string model)
        {
            return _service.GetCaseByModel(model);
        }

        // GET: Cases
        public IActionResult Index()
        {
            return View(_service.GetAllCases());
        }

        // GET: Cases/Details/5
        public IActionResult Details(int? id)
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
        public IActionResult Create([Bind("CaseId,Model,Manufacturer,Price,Type")] Case @case)
        {
            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    @case.ImgUrl = _driveService.UploadFile(image);
                }
                else
                {
                    @case.ImgUrl = Constants.DEFAULT_CASE_IMG;
                }

                _service.InsertCase(@case);
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }

        // GET: Cases/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Edit(int? id)
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
        public IActionResult Edit(int id, [Bind("CaseId,Model,Manufacturer,Price,Type")] Case @case)
        {
            if (id != @case.CaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var image = Request.Form.Files.GetFile("image");
                if (image != null)
                {
                    string oldImg = Request.Form["ImgUrl"];
                    _driveService.DeleteFile(oldImg);
                    @case.ImgUrl = _driveService.UploadFile(image);
                }

                _service.Update(@case);
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }

        // GET: Cases/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public IActionResult Delete(int? id)
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
        public IActionResult DeleteConfirmed(int id)
        {
            var @case = _service.GetCaseById(id);
            string imageUrl = @case.ImgUrl;
            if (imageUrl != Constants.DEFAULT_CASE_IMG)
            {
                _driveService.DeleteFile(@case.ImgUrl);
            }            

            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
