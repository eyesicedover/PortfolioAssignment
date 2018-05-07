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
    public class PostsController : Controller
    {
        public ApplicationDbContext _db;
        public UserManager<ApplicationUser> _userManager;

        public PostsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Posts.Include(p => p.Comments));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateConfirm()
        {
            Post newPost = new Post();
            newPost.Content = Request.Form["content"];
            newPost.PostDate = DateTime.Now;
            _db.Posts.Add(newPost);
            _db.SaveChanges();
            return RedirectToAction("Index", "Posts");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            return View(_db.Posts.FirstOrDefault(x => x.PostId == id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditConfirm()
        {
            Post editPost = new Post();
            editPost.Content = Request.Form["content"];
            editPost.PostDate = Convert.ToDateTime(Request.Form["time"]);
            _db.Posts.Update(editPost);
            _db.SaveChanges();
            return View();
        }

        public IActionResult Details(int id)
        {
            return View(_db.Posts.FirstOrDefault(y => y.PostId == id));
        }

        public IActionResult Delete(int id)
        {
            Post thisPost = _db.Posts.FirstOrDefault(z => z.PostId == id);
            _db.Posts.Remove(thisPost);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            IEnumerable<Post> allPosts = _db.Posts;
            IEnumerable<Comment> allComments = _db.Comments;
            foreach (var post in allPosts)
            {
                _db.Posts.Remove(post);
            }
            foreach (var comment in allComments)
            {
                _db.Comments.Remove(comment);
            }
            return RedirectToAction("Index");
        }
    }
}
