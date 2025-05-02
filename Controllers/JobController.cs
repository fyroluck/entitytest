using entitytest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class JobController : Controller
{
    private readonly AppDbContext _context;

    public JobController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Job/Index
    public async Task<IActionResult> Index()
    {
        var jobs = await _context.Jobs
            .Include(j => j.Employer)  // Include the related employer details
            .ToListAsync();
        return View(jobs);
    }

    // GET: Job/Create
    public IActionResult Create()
    {
        // Populate the ViewBag with a list of employers for the dropdown
        ViewBag.EmployerID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Employers, "EmployerID", "EmployerName");
        return View();
    }

    // POST: Job/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("JobTitle, EmployerID")] Job job)
    {
        if (ModelState.IsValid)
        {
            // Add new job and save to database
            _context.Add(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // If model is invalid, re-populate the dropdown list and return the view with validation errors
        ViewBag.EmployerID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Employers, "EmployerID", "EmployerName", job.EmployerID);
        return View(job);
    }

    // GET: Job/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var job = await _context.Jobs.FindAsync(id);
        if (job == null)
        {
            return NotFound();
        }

        // Populate the dropdown with employers
        ViewBag.EmployerID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Employers, "EmployerID", "EmployerName", job.EmployerID);
        return View(job);
    }

    // POST: Job/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("JobID, JobTitle, EmployerID")] Job job)
    {
        if (id != job.JobID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Update job and save to database
                _context.Update(job);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Jobs.Any(e => e.JobID == job.JobID))
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

        // If model is invalid, re-populate the dropdown list and return the view with validation errors
        ViewBag.EmployerID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Employers, "EmployerID", "EmployerName", job.EmployerID);
        return View(job);
    }

    // GET: Job/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var job = await _context.Jobs
            .Include(j => j.Employer)
            .FirstOrDefaultAsync(m => m.JobID == id);

        if (job == null)
        {
            return NotFound();
        }

        return View(job);
    }

    // POST: Job/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job != null)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
