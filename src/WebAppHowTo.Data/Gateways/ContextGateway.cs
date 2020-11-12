using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAppHowTo.Data.Model;

namespace WebAppHowTo.Data.Gateways
{
    public class ContextGateway : IContextGateway
    {
        private readonly PerryMasonContext _context;
        private readonly ILogger<PerryMasonContext> _logger;

        public ContextGateway(PerryMasonContext context, ILogger<PerryMasonContext> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<List<Customer>> GetCostumers() => await _context.Customers.OrderByDescending(c => c.Name).ToListAsync();
    }
}