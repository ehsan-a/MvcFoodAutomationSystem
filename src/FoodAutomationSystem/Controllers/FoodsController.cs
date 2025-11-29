using Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Type,Price,Calories,Available")] Food food)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(food);
                return RedirectToAction("FoodManagement", "Admin");
            }
            return RedirectToAction("FoodManagement", "Admin");
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Type,Price,Calories,Available")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(food);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await FoodExists(food.Id) == false)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("FoodManagement", "Admin");
        }

        private async Task<bool> FoodExists(int id)
        {
            return await _service.ExistsAsync(id);
        }
    }
}
