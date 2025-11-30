using Domain.Entities;
using Application.Interfaces;
using FoodAutomationSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodAutomationSystem.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Description,Type,Status,UserId")] Transaction transaction, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(transaction, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment([Bind("UserId,FoodMenuId,Amount")] ReservationDataViewModel reservationDataViewModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var transaction = new Transaction
                {
                    Amount = reservationDataViewModel.Amount,
                    Description = "Reservation",
                    Type = TransactionType.Reservation,
                    Status = TransactionStatus.Success,
                    UserId = reservationDataViewModel.UserId,
                };
                await _service.CreateAsync(transaction, cancellationToken);
                reservationDataViewModel.TransactionId = transaction.Id;
                return View("PostRedirect", reservationDataViewModel);
            }
            return View();
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Date,Description,Type,Status,UserId")] Transaction transaction, CancellationToken cancellationToken)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(transaction, cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TransactionExists(transaction.Id, cancellationToken) == false)
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
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TransactionExists(int id, CancellationToken cancellationToken)
        {
            return await _service.ExistsAsync(id, cancellationToken);
        }
    }
}
