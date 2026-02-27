using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0211.Models;

namespace Mission08_Team0211.Controllers;

public class HomeController : Controller
{
    private readonly IQuadrantsRepository _repo;

    public HomeController(IQuadrantsRepository repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        return View();
    }

    // STEP 3: Quadrants View (show ONLY incomplete tasks)
    public IActionResult Quadrants()
    {
        var tasks = _repo.ToDoTasks
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
        ViewBag.Categories = _repo.Categories
            .OrderBy(c => c.CategoryName)
            .ToList();

        return View(new ToDoTask());
    }

    // STEP 2: Add Task (POST)
    [HttpPost]
    public IActionResult AddTask(ToDoTask response)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _repo.Categories
                .OrderBy(c => c.CategoryName)
                .ToList();

            return View(response);
        }

        // Default to not completed when creating
        response.Completed = false;

        _repo.AddTask(response);

        return RedirectToAction("Quadrants");
    }

    // Edit (GET) - reuse AddTask view
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var recordToEdit = _repo.ToDoTasks
            .Single(x => x.ToDoTaskId == id);

        ViewBag.Categories = _repo.Categories
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
            ViewBag.Categories = _repo.Categories
                .OrderBy(c => c.CategoryName)
                .ToList();

            return View("AddTask", updatedInfo);
        }

        _repo.UpdateTask(updatedInfo);

        return RedirectToAction("Quadrants");
    }

    // Delete (POST) - no confirmation view required
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var recordToDelete = _repo.ToDoTasks
            .SingleOrDefault(x => x.ToDoTaskId == id);

        if (recordToDelete == null)
        {
            return RedirectToAction("Quadrants");
        }

        _repo.DeleteTask(recordToDelete);

        return RedirectToAction("Quadrants");
    }

    // Mark Complete (POST)
    [HttpPost]
    public IActionResult Complete(int id)
    {
        var record = _repo.ToDoTasks.Single(x => x.ToDoTaskId == id);
        record.Completed = true;

        _repo.UpdateTask(record);

        return RedirectToAction("Quadrants");
    }

}