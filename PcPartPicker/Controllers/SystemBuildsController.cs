using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Data;
using PcPartPicker.Data.Models;

namespace PcPartPicker.Controllers
{
    public class SystemBuildsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemBuildsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemBuilds
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemBuilds.ToListAsync());
        }

        // GET: SystemBuilds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = await _context.SystemBuilds
                .Include(b => b.Cpu)
                .Include(b => b.Case)
                .Include(b => b.Gpu)
                .Include(b => b.Motherboard)
                .Include(b => b.Ram)
                .Include(b => b.Storage)
                .FirstOrDefaultAsync(m => m.SystemBuildId == id);
            if (systemBuild == null)
            {
                return NotFound();
            }

            return View(systemBuild);
        }
        
        [Authorize(Roles = "Admin, Vendor")]
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
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            SystemBuild systemBuild = new SystemBuild();
            systemBuild.Cpu = _context.Cpus.FirstOrDefault(x => x.Model == Request.Form["cpus"].ToString());
            systemBuild.Gpu = _context.Gpus.FirstOrDefault(x => x.Model == Request.Form["gpus"].ToString());
            systemBuild.Case = _context.Cases.FirstOrDefault(x => x.Model == Request.Form["cases"].ToString());
            systemBuild.Motherboard = _context.Motherboards.FirstOrDefault(x => x.Model == Request.Form["motherboards"].ToString());
            systemBuild.Storage = _context.Storages.FirstOrDefault(x => x.Model == Request.Form["storages"].ToString());
            systemBuild.Ram = _context.Rams.FirstOrDefault(x => x.Model == Request.Form["rams"].ToString());
            systemBuild.Price = decimal.Parse(Request.Form["Price"].ToString());
            systemBuild.Name = Request.Form["Name"].ToString();
            systemBuild.Description = Request.Form["Description"].ToString();
            if (ModelState.IsValid) // remove?
            {
                _context.Add(systemBuild);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemBuild);
        }

        // GET: SystemBuilds/Edit/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = await _context.SystemBuilds
                .Include(b => b.Cpu)
                .Include(b => b.Case)
                .Include(b => b.Gpu)
                .Include(b => b.Motherboard)
                .Include(b => b.Ram)
                .Include(b => b.Storage)
                .FirstOrDefaultAsync(m => m.SystemBuildId == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("SystemBuildId,Price")] SystemBuild systemBuild)
        {
            if (id != systemBuild.SystemBuildId)
            {
                return NotFound();
            }


            systemBuild.Cpu = _context.Cpus.FirstOrDefault(x => x.Model == Request.Form["cpus"].ToString());
            systemBuild.Gpu = _context.Gpus.FirstOrDefault(x => x.Model == Request.Form["gpus"].ToString());
            systemBuild.Case = _context.Cases.FirstOrDefault(x => x.Model == Request.Form["cases"].ToString());
            systemBuild.Motherboard = _context.Motherboards.FirstOrDefault(x => x.Model == Request.Form["motherboards"].ToString());
            systemBuild.Storage = _context.Storages.FirstOrDefault(x => x.Model == Request.Form["storages"].ToString());
            systemBuild.Ram = _context.Rams.FirstOrDefault(x => x.Model == Request.Form["rams"].ToString());
            systemBuild.Price = systemBuild.Cpu.Price + systemBuild.Gpu.Price + systemBuild.Motherboard.Price + systemBuild.Ram.Price + systemBuild.Storage.Price + systemBuild.Case.Price;
            systemBuild.Name = Request.Form["Name"].ToString();
            systemBuild.Description = Request.Form["Description"].ToString();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemBuild);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemBuildExists(systemBuild.SystemBuildId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(systemBuild);
        }

        // GET: SystemBuilds/Delete/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = await _context.SystemBuilds
                .FirstOrDefaultAsync(m => m.SystemBuildId == id);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemBuild = await _context.SystemBuilds.FindAsync(id);
            _context.SystemBuilds.Remove(systemBuild);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemBuildExists(int id)
        {
            return _context.SystemBuilds.Any(e => e.SystemBuildId == id);
        }
    }
}
