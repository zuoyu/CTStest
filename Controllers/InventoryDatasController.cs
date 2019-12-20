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
    public class InventoryDatasController : Controller
    {
        private readonly AppDbContext _context;

        public InventoryDatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InventoryDatas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.InventoryDatas.Include(i => i.GroupData);
            return View(await appDbContext.ToListAsync());
        }

        // GET: InventoryDatas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryData = await _context.InventoryDatas
                .Include(i => i.GroupData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryData == null)
            {
                return NotFound();
            }

            return View(inventoryData);
        }

        // GET: InventoryDatas/Create
        public IActionResult Create()
        {
            ViewData["GroupDataId"] = new SelectList(_context.GroupDatas, "Id", "Id");
            return View();
        }

        // POST: InventoryDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Count,Unit_Price,GroupDataId")] InventoryData inventoryData)
        {
            if (ModelState.IsValid)
            {
                inventoryData.Id = Guid.NewGuid();
                _context.Add(inventoryData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupDataId"] = new SelectList(_context.GroupDatas, "Id", "Id", inventoryData.GroupDataId);
            return View(inventoryData);
        }

        // GET: InventoryDatas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryData = await _context.InventoryDatas.FindAsync(id);
            if (inventoryData == null)
            {
                return NotFound();
            }
            ViewData["GroupDataId"] = new SelectList(_context.GroupDatas, "Id", "Id", inventoryData.GroupDataId);
            return View(inventoryData);
        }

        // POST: InventoryDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Count,Unit_Price,GroupDataId")] InventoryData inventoryData)
        {
            if (id != inventoryData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryDataExists(inventoryData.Id))
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
            ViewData["GroupDataId"] = new SelectList(_context.GroupDatas, "Id", "Id", inventoryData.GroupDataId);
            return View(inventoryData);
        }

        // GET: InventoryDatas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryData = await _context.InventoryDatas
                .Include(i => i.GroupData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryData == null)
            {
                return NotFound();
            }

            return View(inventoryData);
        }

        // POST: InventoryDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var inventoryData = await _context.InventoryDatas.FindAsync(id);
            _context.InventoryDatas.Remove(inventoryData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryDataExists(Guid id)
        {
            return _context.InventoryDatas.Any(e => e.Id == id);
        }
    }
}
