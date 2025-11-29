using Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodAutomationSystem.Controllers
{
    public class MenusController : Controller
    {
        private readonly IMenuService _service;

        public MenusController(IMenuService service)
        {
            _service = service;
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeekStartDate")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(menu);
                return RedirectToAction("MenuManagement", "Admin");
            }
            return RedirectToAction("MenuManagement", "Admin");
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeekStartDate")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(menu);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await MenuExists(menu.Id) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MenuManagement", "Admin");
            }
            return RedirectToAction("MenuManagement", "Admin");
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("MenuManagement", "Admin");
        }

        private async Task<bool> MenuExists(int id)
        {
            return await _service.ExistsAsync(id);
        }
    }
}
