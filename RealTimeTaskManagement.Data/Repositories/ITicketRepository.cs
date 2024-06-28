using RealTimeTaskManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Data.Repositories
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
        void Add(Ticket ticket);
    }
}
