using entitytest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EmployerController : Controller
{
    private readonly AppDbContext _context;

    public EmployerController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Employer
    public async Task<IActionResult> Index()
    {
        var employers = await _context.Employers
            .Include(e => e.Jobs)
            .ToListAsync();
        return View(employers);
    }

    // GET: Employer/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employer/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EmployerName")] Employer employer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(employer);
    }

    // GET: Employer/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employer = await _context.Employers.FindAsync(id);
        if (employer == null)
        {
            return NotFound();
        }
        return View(employer);
    }

    // POST: Employer/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EmployerID,EmployerName")] Employer employer)
    {
        if (id != employer.EmployerID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(employer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployerExists(employer.EmployerID))
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
        return View(employer);
    }

    // GET: Employer/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employer = await _context.Employers
            .FirstOrDefaultAsync(m => m.EmployerID == id);
        if (employer == null)
        {
            return NotFound();
        }

        return View(employer);
    }

    // POST: Employer/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var employer = await _context.Employers.FindAsync(id);
        _context.Employers.Remove(employer);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EmployerExists(int id)
    {
        return _context.Employers.Any(e => e.EmployerID == id);
    }
}
