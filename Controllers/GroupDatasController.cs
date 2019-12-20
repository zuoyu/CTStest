using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hometest.Models;

namespace hometest.Controllers
{
    public class GroupDatasController : Controller
    {
        private readonly AppDbContext _context;

        public GroupDatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GroupDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroupDatas.ToListAsync());
        }

        // GET: GroupDatas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupData = await _context.GroupDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupData == null)
            {
                return NotFound();
            }

            return View(groupData);
        }

        // GET: GroupDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GroupData groupData)
        {
            if (ModelState.IsValid)
            {
                groupData.Id = Guid.NewGuid();
                _context.Add(groupData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupData);
        }

        // GET: GroupDatas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupData = await _context.GroupDatas.FindAsync(id);
            if (groupData == null)
            {
                return NotFound();
            }
            return View(groupData);
        }

        // POST: GroupDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] GroupData groupData)
        {
            if (id != groupData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupDataExists(groupData.Id))
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
            return View(groupData);
        }

        // GET: GroupDatas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupData = await _context.GroupDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupData == null)
            {
                return NotFound();
            }

            return View(groupData);
        }

        // POST: GroupDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var groupData = await _context.GroupDatas.FindAsync(id);
            _context.GroupDatas.Remove(groupData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupDataExists(Guid id)
        {
            return _context.GroupDatas.Any(e => e.Id == id);
        }
    }
}
