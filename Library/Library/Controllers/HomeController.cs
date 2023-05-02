using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Library.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        private LibraryContext context;
        public HomeController(LibraryContext ctx) => context = ctx;

        public IActionResult Index(string id)
        {
            // load current filters and data needed for filter drop downs in ViewBag
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Genres = context.Genres.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;

            IQueryable<Book> query = context.Books
                .Include(t => t.Genre).Include(t => t.Status);
            if (filters.HasGenre)
            {
                query = query.Where(t => t.GenreId == filters.GenreId);
            }
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }
            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                    query = query.Where(t => t.DueDate < today);
                else if (filters.IsFuture)
                    query = query.Where(t => t.DueDate > today);
                else if (filters.IsToday)
                    query = query.Where(t => t.DueDate == today);
            }
            var tasks = query.OrderBy(t => t.DueDate).ToList();
            return View(tasks);
        }

        public IActionResult Add()
        {
            ViewBag.Genres = context.Genres.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Book book)
        {
            if (ModelState.IsValid)
            {                               
                Random rand = new Random(13);                
                book.ISBN = Convert.ToString(rand.Next());
                book.StatusId = "available";
                if ((!book.DueDate.HasValue))
                {
                    book.DueDate = DateTime.Now.AddDays(15);
                }
                context.Books.Add(book);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Genres = context.Genres.ToList();
                ViewBag.Statuses = context.Statuses.ToList();
                return View(book);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]

        public IActionResult Edit([FromRoute] string id, Book selected)
        {
            if (selected.StatusId == null)
            {

                context.Books.Remove(selected);

            }
            else
            {
                string newStatusId = selected.StatusId;
                selected = context.Books.Find(selected.Id);
                selected.StatusId = newStatusId;
                if (newStatusId == "checked")
                {
                    selected.Owner = User.Identity.Name;
                    selected.DueDate = DateTime.Now.AddDays(15);
                }
                if (newStatusId == "returned")
                {
                    selected.DueDate = new DateTime();
                }
                if (newStatusId == "available")
                {
                    selected.Owner = "";
                }

                context.Books.Update(selected);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }

        [HttpGet]
        public ActionResult Change([FromRoute] int id)
        {
            ViewBag.Users = context.Users.OrderBy(u => u.UserName).ToList();
            ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Name).ToList();
            var book = context.Books.Find(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Change([FromRoute] string id, Book selected)
        {
            if (ModelState.IsValid)
            {
                context.Books.Update(selected);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(selected);
            }

        }




    }
}