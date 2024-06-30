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

        public IEnumerable<Ticket> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public void Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }
    }
}
