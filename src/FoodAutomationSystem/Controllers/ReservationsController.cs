using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Interfaces;

namespace FoodAutomationSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _service;
        private readonly UserManager<User> _userManager;
        public ReservationsController(IReservationService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodMenuId,UserId,TransactionId")] Reservation reservation, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(reservation, cancellationToken);
                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("PostRedirect", reservation);
                }
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodMenuId,UserId,Date")] Reservation reservation, CancellationToken cancellationToken)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(reservation, cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ReservationExists(reservation.Id, cancellationToken) == false)
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
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReservationExists(int id, CancellationToken cancellationToken)
        {
            return await _service.ExistsAsync(id, cancellationToken);
        }
    }
}
