using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PcPartPicker.Areas.Component.Models;
using PcPartPicker.Data;
using PcPartPicker.Definitions;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Areas.Component
{
    [Area("Component")]
    public class SystemBuildsController : Controller
    {
        private readonly ISystemBuildService _service;
        private readonly IGoogleDriveService _driveService;

        public SystemBuildsController(ISystemBuildService service, IGoogleDriveService driveService)
        {
            _service = service;
            _driveService = driveService;
        }

        // GET: SystemBuilds
        public IActionResult Index()
        {
            return View(_service.GetAllSystemBuilds());
        }

        public IEnumerable<SystemBuildVM_Preview> GetSystemBuilds(int? skip, int? take)
        {
            var builds = _service.GetAllSystemBuilds(skip, take).ToList();
            List<SystemBuildVM_Preview> vms = new List<SystemBuildVM_Preview>();
            for (int i = 0; i < builds.Count; i++)
            {
                var buildVM = new SystemBuildVM_Preview();
                buildVM.Id = builds[i].SystemBuildId;
                buildVM.Name = builds[i].Name;
                buildVM.Description = builds[i].Description;
                buildVM.Price = builds[i].Price;
                vms.Add(buildVM);
            }

            return vms;
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
            var image = Request.Form.Files.GetFile("image");
            string imgUrl = string.Empty;
            if (image != null)
            {
                imgUrl = _driveService.UploadFile(image);
            }
            _service.InsertSystemBuild(cpuModel, caseModel, gpuModel, memoryOptionModel, motherboardModel, storageOptionModel, price, name, description, imgUrl);
            
            if (ModelState.IsValid)
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
                var image = Request.Form.Files.GetFile("image");
                string imageUrl = string.Empty;
                if (image != null)
                {
                    _driveService.DeleteFile(Request.Form["ImgUrl"]);
                    imageUrl = _driveService.UploadFile(image);
                }
                else
                {
                    imageUrl = Constants.DEFAULT_BUILD_IMG;
                }

                _service.Update(cpuModel, caseModel, gpuModel, memoryOptionModel, motherboardModel, storageOptionModel, name, description, id, imageUrl);
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
            var build = _service.GetSystemBuildById(id);
            var buildImage = build.ImgUrl;
            if (buildImage != Constants.DEFAULT_BUILD_IMG)
            {
                _driveService.DeleteFile(buildImage);
            }

            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
