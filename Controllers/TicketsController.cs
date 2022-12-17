using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperSuperSimpleHelpDesk.Data;
using SuperSuperSimpleHelpDesk.Entities;
using SuperSuperSimpleHelpDesk.Models.Ticket;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace SuperSuperSimpleHelpDesk
{
    public class TicketsController : Controller
    {
        private readonly DataContext _context;

        public TicketsController(DataContext context)
        {
            _context = context;
        }

        // GET: Listagem dos tickets
        public async Task<IActionResult> Index()
        {
            var allTickets = await _context.Ticket.ToListAsync();
            var allListTicketsViewModel = new List<ListTicketViewModel>();

            foreach (var ticket in allTickets)
            {
                var listTicketViewModel = new ListTicketViewModel()
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Message = ticket.Message,
                    AssociatedPhone = ticket.AssociatedPhone,
                    CreatedAt = ticket.CreatedAt.ToString("G", new CultureInfo("pt-BR")),
                    UpdatedAt = ticket.UpdatedAt != null ? ticket.UpdatedAt.Value.ToString("G", new CultureInfo("pt-BR")) : "",
                    Owner = ticket.Owner,
                    Place = ticket.Place,
                    Status = ticket.Status.GetType()
                            .GetMember(ticket.Status.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .Name
                };

                allListTicketsViewModel.Add(listTicketViewModel);
            }

            return View(allListTicketsViewModel);
        }

        // GET: Cria um ticket
        public IActionResult Create()
        {

            return View(new CreateTicketViewModel());
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTicketViewModel openTicketViewModel)
        {
            if (!ModelState.IsValid)
                return View(openTicketViewModel);
           
            var newTicket = new Ticket()
            {
                CreatedAt = DateTime.Now,
                Title = openTicketViewModel.Title,
                Message = openTicketViewModel.Message,
                Owner = openTicketViewModel.Owner,
                Place = openTicketViewModel.Place,
                AssociatedPhone = openTicketViewModel.AssociatedPhone,
                Status = TicketStatus.Open
            };

            _context.Add(newTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: busca o ticket que será editado
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var editTicketViewModel = new EditTicketViewModel()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Message = ticket.Message,
                AssociatedPhone = ticket.AssociatedPhone,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt,
                Owner = ticket.Owner,
                Place = ticket.Place,
                Status = (TicketStatusViewModel)ticket.Status
            };

            return View(editTicketViewModel);
        }

        // POST: Edita o ticket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTicketViewModel editTicket)
        {
            if (id != editTicket.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(editTicket);

            try
            {
                var editedTicket = new Ticket()
                {
                    Id = editTicket.Id,
                    CreatedAt = editTicket.CreatedAt,
                    UpdatedAt = DateTimeOffset.Now,
                    Title = editTicket.Title,
                    Message = editTicket.Message,
                    Owner = editTicket.Owner,
                    Place = editTicket.Place,
                    AssociatedPhone = editTicket.AssociatedPhone,
                    Status = (TicketStatus)editTicket.Status
                };

                _context.Update(editedTicket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(editTicket.Id))
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

        // GET: Deleta o ticket
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Confirma a deleção de um ticket
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'DataContext.Ticket'  is null.");
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
