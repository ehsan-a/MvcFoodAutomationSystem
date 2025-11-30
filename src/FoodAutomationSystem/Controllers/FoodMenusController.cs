using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;

namespace FoodAutomationSystem.Controllers
{
    public class FoodMenusController : Controller
    {
        private readonly IFoodMenuService _service;

        public FoodMenusController(IFoodMenuService service)
        {
            _service = service;
        }

        // POST: FoodMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,FoodId,UnitPrice,DayOfWeek")] FoodMenu foodMenu, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(foodMenu, cancellationToken);
                return RedirectToAction("MenuManagement", "Admin");
            }
            return RedirectToAction("MenuManagement", "Admin");
        }

        // POST: FoodMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuId, FoodId, UnitPrice, DayOfWeek")] FoodMenu foodMenu, CancellationToken cancellationToken)
        {
            if (id != foodMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(foodMenu, cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await FoodMenuExists(foodMenu.Id, cancellationToken) == false)
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

        // POST: FoodMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return RedirectToAction("MenuManagement", "Admin");
        }

        private async Task<bool> FoodMenuExists(int id, CancellationToken cancellationToken)
        {
            return await _service.ExistsAsync(id, cancellationToken);
        }
    }
}
