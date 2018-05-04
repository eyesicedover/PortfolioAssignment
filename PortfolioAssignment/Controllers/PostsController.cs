using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortfolioAssignment.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioAssignment.Controllers
{
    public class PostsController : Controller
    {
        public AppDbContext db;
        public UserManager<ApplicationUser> userManager;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(db.Posts);
        }
    }
}
