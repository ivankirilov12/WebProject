using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PcPartPicker.Data;
using System.Collections.Generic;
using System.Linq;

namespace PcPartPicker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(ApplicationDbContext context, UserManager<IdentityUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }
               
        public IActionResult Index()
        {
            AdminPanelVm vm = new AdminPanelVm();
            vm.Users = _context.Users.Select(user => user.Email).ToList();
            vm.Roles = _context.Roles.Select(role => role.Name).ToList();
            return View("Index", vm);
        }

        public IActionResult AssignRole()
        {
            string username = this.Request.Form["users"].ToString();
            string roleName = this.Request.Form["roles"].ToString();
            var user = _context.Users.FirstOrDefault(a => a.Email == username);
            _userManager.AddToRoleAsync(user, roleName);

            return RedirectToAction("Index");
        }
    }
}
public class AdminPanelVm
{
    public IEnumerable<string> Users { get; set; }
    public IEnumerable<string> Roles { get; set; }

}