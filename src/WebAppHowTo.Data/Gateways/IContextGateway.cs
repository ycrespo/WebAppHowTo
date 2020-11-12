using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppHowTo.Data.Model;

namespace WebAppHowTo.Data.Gateways
{
    public interface IContextGateway
    {
        Task<List<Customer>> GetCostumers();
    }
}