using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PortfolioAssignment.Models;


namespace PortfolioAssignment.Controllers
{
    public class CommentsController : Controller
    {
        public ApplicationDbContext _db;
        public UserManager<ApplicationUser> _userManager;

        public CommentsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Create()
        {
            Comment newComment = new Comment(int.Parse(Request.Form["postId"]), Request.Form["author"], Request.Form["content"]);
            _db.Comments.Add(newComment);
            _db.SaveChanges();
            return RedirectToAction("Details", "Posts", new { id = newComment.PostId });
        }
    }
}