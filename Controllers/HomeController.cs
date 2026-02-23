using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0211.Models;

namespace Mission08_Team0211.Controllers;

public class HomeController : Controller
{
    private readonly QuadrantsContext _context;

    public HomeController(QuadrantsContext temp)
    {
        _context = temp;
    }

    public IActionResult Index()
    {
        return View();
    }

    // STEP 3: Quadrants View (show ONLY incomplete tasks)
    public IActionResult Quadrants()
    {
        var tasks = _context.ToDoTasks
            .Include(t => t.Category)
            .Where(t => t.Completed == false)
            .OrderBy(t => t.Quadrant)
            .ThenBy(t => t.DueDate)
            .ToList();

        return View(tasks);
    }

    // STEP 2: Add Task (GET)
    [HttpGet]
    public IActionResult AddTask()
    {
        ViewBag.Categories = _context.Categories
            .OrderBy(c => c.CategoryName)
            .ToList();

        return View(new ToDoTask());
    }

    // STEP 2: Add Task (POST)
    [HttpPost]
    public IActionResult AddTask(ToDoTask response)
    {
        ViewBag.Categories = _context.Categories
            .OrderBy(c => c.CategoryName)
            .ToList();

        if (!ModelState.IsValid)
        {
            return View(response);
        }

        // Default to not completed when creating
        response.Completed = false;

        _context.ToDoTasks.Add(response);
        _context.SaveChanges();

        return RedirectToAction("Quadrants");
    }

    // Edit (GET) - reuse AddTask view like DateMe
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var recordToEdit = _context.ToDoTasks
            .Single(x => x.ToDoTaskId == id);

        ViewBag.Categories = _context.Categories
            .OrderBy(c => c.CategoryName)
            .ToList();

        return View("AddTask", recordToEdit);
    }

    // Edit (POST)
    [HttpPost]
    public IActionResult Edit(ToDoTask updatedInfo)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(c => c.CategoryName)
                .ToList();

            return View("AddTask", updatedInfo);
        }

        _context.Update(updatedInfo);
        _context.SaveChanges();

        return RedirectToAction("Quadrants");
    }

    // Delete confirmation (GET)
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var recordToDelete = _context.ToDoTasks
            .Include(t => t.Category)
            .Single(x => x.ToDoTaskId == id);

        return View(recordToDelete);
    }

    // Delete (POST)
    [HttpPost]
    public IActionResult Delete(ToDoTask task)
    {
        _context.ToDoTasks.Remove(task);
        _context.SaveChanges();

        return RedirectToAction("Quadrants");
    }

    // Mark Complete (POST)
    [HttpPost]
    public IActionResult Complete(int id)
    {
        var record = _context.ToDoTasks.Single(x => x.ToDoTaskId == id);
        record.Completed = true;

        _context.Update(record);
        _context.SaveChanges();

        return RedirectToAction("Quadrants");
    }
}