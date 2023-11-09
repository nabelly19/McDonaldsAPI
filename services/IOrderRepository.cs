using System.Collections.Generic;
using System.Threading.Tasks;
using apimcdonalds.Model;

namespace McDonaldsAPI.Services;

public interface IOrderRepository
{
    Task<int> CreateORder(int storeId);
    Task CancelOrder(int orderId);
    Task<List<Product>> GetMenu(int orderId);
    Task<List<Product>> GetOrderItems(int orderId);
    Task RemoveItem(int orderId, int itemId);
    Task FinishOrder(int orderId);
    Task DeliveryOrder(int orderId);
}