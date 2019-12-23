using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hometest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hometest.Controllers
{
    public class AppUpdateController : Controller
    {
        private readonly AppDbContext _context;

        public AppUpdateController(AppDbContext context)
        {
            _context = context;
        }
        

        [HttpPost]
 
        public async Task<JsonResult> Create([Bind("Id,Name,Count,Unit_Price,GroupDataId,Warning_Count")] InventoryData inventoryData)
        {
            if (ModelState.IsValid)
            {
                inventoryData.Id = Guid.NewGuid();
                _context.Add(inventoryData);
                await _context.SaveChangesAsync();
                return Json("Form Submitted");
            }
            var errors = string.Join("<br />", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).Select(s => s.ErrorMessage).ToArray());
            return Json(errors);
        }

        [HttpPost]
       
        public async Task<JsonResult> Edit( [Bind("Id,Name,Count,Unit_Price,GroupDataId,Warning_Count")] InventoryData inventoryData)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var errors1 = string.Join("<br />", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).Select(s => s.ErrorMessage).ToArray());
                    return Json(errors1);
                }
                return Json("Form Updated");
            }

            var errors = string.Join("<br />", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).Select(s => s.ErrorMessage).ToArray());
            return Json(errors);
        }
    }
}