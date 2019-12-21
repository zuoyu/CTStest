using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hometest.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Index(int? page, string inventory_name)
        {
            var pageNumaber = page ?? 1;
            int pageSize = 10;

            ViewData["search_Data"] = inventory_name;
            if (inventory_name == null)
            {
                var appDbContext = _context.InventoryDatas.OrderByDescending(s => s.Count<s.Warning_Count ).Include(j => j.GroupData);
                var datalist = await appDbContext.ToListAsync();
                var onePageJobs = datalist.ToPagedList(pageNumaber, pageSize);
                return View(onePageJobs);
            }
            else
            {
                var appDbContext = _context.InventoryDatas.Where(s => s.Name == inventory_name).OrderByDescending(s => s.Count < s.Warning_Count).Include(j => j.GroupData);
                var datalist = await appDbContext.ToListAsync();
                var onePageJobs = datalist.ToPagedList(pageNumaber, pageSize);
                return View(onePageJobs);
            }

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
            ViewData["GroupDataId"] = new SelectList(_context.GroupDatas, "Id", "Name");
            return View();
        }

        // POST: InventoryDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Count,Unit_Price,GroupDataId,Warning_Count")] InventoryData inventoryData)
        {
            if (ModelState.IsValid)
            {
                inventoryData.Id = Guid.NewGuid();
                _context.Add(inventoryData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupDataId"] = new SelectList(_context.GroupDatas, "Id", "Name", inventoryData.GroupDataId);
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
            ViewData["GroupDataId"] = new SelectList(_context.GroupDatas, "Id", "Name", inventoryData.GroupDataId);
            return View(inventoryData);
        }

        // POST: InventoryDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Count,Unit_Price,GroupDataId,Warning_Count")] InventoryData inventoryData)
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
            ViewData["GroupDataId"] = new SelectList(_context.GroupDatas, "Id", "Name", inventoryData.GroupDataId);
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

        [HttpGet]
        public ActionResult AllGetInventory()
        {

            var Inventory_data = _context.InventoryDatas.Select(s => new {
                s.Id,
                s.Name,
                s.Count,
                s.GroupData,
                s.Warning_Count,
            }).ToList();

            return Content(JsonConvert.SerializeObject(Inventory_data), "application/json");
        }


        [HttpGet]
        public ActionResult GetInventory(Guid? Id)
        {

            var Inventory_data = _context.InventoryDatas.Where(x => x.Id == Id).Select(s => new {
                s.Id,
                s.Name,
                s.Count,
                s.GroupData,
                s.Warning_Count,
            }).ToList();



            return Content(JsonConvert.SerializeObject(Inventory_data), "application/json");
        }

        [HttpGet]
        public ActionResult GetGroup(Guid? Id)
        {
            var Group = _context.InventoryDatas.Where(x => x.GroupDataId == Id).Select(s => new {
                s.Id,
                s.Name,
                s.Count,
                s.GroupData,
                s.Warning_Count,
            }).ToList();

            return Content(JsonConvert.SerializeObject(Group), "application/json");
        }

    }
}
