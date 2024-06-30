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

        public IEnumerable<TicketEntity> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public void Add(TicketEntity ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }
    }
}
