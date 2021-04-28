using Core.Dto;
using Core.Entities;

namespace Core.Interfaces.Gateways.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        OrderDto GenerateOrderNo();
    }
}
