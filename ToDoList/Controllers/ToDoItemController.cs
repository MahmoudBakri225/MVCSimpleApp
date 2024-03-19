using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoItemController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Create()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddDays(1);

            string note = "Store it also in cookie (1 day)";
            Response.Cookies.Append("note", note, cookieOptions);
            ViewData["msg"] = Request.Cookies["note"]; 
            return View();
        }
       

        public IActionResult Items(string userName)
        {
            var listItem = context.TodoItems.ToList();
            ViewData["UserName"] = userName;
            return View(listItem);
        }

        public IActionResult CreateNew()
        {
            return View();
        }

        public IActionResult SaveNew(string title, string description, DateTime deadline)
        {
            if (!string.IsNullOrEmpty(title) && deadline != default)
            {
                context.TodoItems.Add(new ListItem { Title = title, Description = description, Deadline = deadline });
                context.SaveChanges();
                TempData["ConfirmationMessage"] = $"Created Example Todo '{title}' successfully.";
            }

            var listItem = context.TodoItems.ToList();
            return View("Items", listItem);
        }

        public IActionResult Edit(int id)
        {
            var listItem = context.TodoItems.Find(id);
            return View(listItem);
        }

        public IActionResult SaveEdit(ListItem editedItem)
        {
            context.TodoItems.Update(editedItem);
            context.SaveChanges();
            TempData["ConfirmationMessage"] = "Item updated successfully.";
            return View("Items", context.TodoItems.ToList());
        }

        public IActionResult Delete(int id)
        {
            var listItem = context.TodoItems.Find(id);
            if (listItem != null)
            {
                context.TodoItems.Remove(listItem);
                context.SaveChanges();
                TempData["ConfirmationMessage"] = "Item deleted successfully.";
            }
            return View("Items", context.TodoItems.ToList());
        }

    }
}
