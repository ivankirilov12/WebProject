using System.Linq;
using System.Threading.Tasks;
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
            return View(await _context.SystemBuild.ToListAsync());
        }

        // GET: SystemBuilds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = await _context.SystemBuild
                .FirstOrDefaultAsync(m => m.SystemBuildId == id);
            if (systemBuild == null)
            {
                return NotFound();
            }

            return View(systemBuild);
        }

        // GET: SystemBuilds/Create
        public IActionResult Create()
        {
            //var cpus = _context.Cpus.Select(x => x.Model).ToList();
            //var gpus = _context.Gpus.Select(x => x.Model).ToList();
            //var cases = _context.Cases.Select(x => x.Model).ToList();
            //var rams = _context.Rams.Select(x => x.Model).ToList();
            //var motherboards = _context.Motherboard.Select(x => x.Model).ToList();
            //var storages = _context.Storages.Select(x => x.Model).ToList();
            return View();
        }

        // POST: SystemBuilds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SystemBuildId,Price")] SystemBuild systemBuild)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemBuild);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemBuild);
        }

        // GET: SystemBuilds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = await _context.SystemBuild.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("SystemBuildId,Price")] SystemBuild systemBuild)
        {
            if (id != systemBuild.SystemBuildId)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemBuild = await _context.SystemBuild
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemBuild = await _context.SystemBuild.FindAsync(id);
            _context.SystemBuild.Remove(systemBuild);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemBuildExists(int id)
        {
            return _context.SystemBuild.Any(e => e.SystemBuildId == id);
        }
    }
}
