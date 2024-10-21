using Microsoft.EntityFrameworkCore;
using RealTimeTaskManagement.Data.Context;
using RealTimeTaskManagement.Data.Entities;

namespace RealTimeTaskManagement.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly RealTimeTaskManagementDbContext _context;

        public TicketRepository(RealTimeTaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketEntity>> GetAll()
        {
            return await _context.Tickets.ToListAsync();
        }

        public void Add(TicketEntity ticket)
        {
            ticket.EnteredOn = DateTime.UtcNow;
            ticket.ModifiedOn = DateTime.UtcNow;
            ticket.LoggedMinutes = TimeSpan.FromMinutes(ticket.LoggedMinutes.Days);
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }
    }
}
