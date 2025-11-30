using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FoodAutomationSystem.Controllers
{
    public class FoodsController : Controller
    {
        private readonly IFoodService _service;

        public FoodsController(IFoodService service)
        {
            _service = service;
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Type,Price,Calories,Available")] Food food, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(food, cancellationToken);
                return RedirectToAction("FoodManagement", "Admin");
            }
            return RedirectToAction("FoodManagement", "Admin");
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Type,Price,Calories,Available")] Food food, CancellationToken cancellationToken)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(food, cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await FoodExists(food.Id, cancellationToken) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FoodManagement", "Admin");
            }
            return RedirectToAction("FoodManagement", "Admin");
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return RedirectToAction("FoodManagement", "Admin");
        }

        private async Task<bool> FoodExists(int id, CancellationToken cancellationToken)
        {
            return await _service.ExistsAsync(id, cancellationToken);
        }
    }
}
