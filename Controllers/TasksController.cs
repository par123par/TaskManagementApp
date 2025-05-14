using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers;
public class TasksController : Controller
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var tasks = _context.Tasks.ToList();
        return View(tasks);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TaskManagementApp.Models.Task task)
    {
        if (ModelState.IsValid)
        {
            _context.Add(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var task = _context.Tasks.Find(id);
        if (task == null) return NotFound();
        return View(task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, TaskManagementApp.Models.Task task)
    {
        if (id != task.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        var task = _context.Tasks.Find(id);
        if (task == null) return NotFound();
        return View(task);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}