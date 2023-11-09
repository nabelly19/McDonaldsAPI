using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using apimcdonalds.Model;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System;
using System.Data;

namespace McDonaldsAPI.Services;

public class OrderRepository : IOrderRepository
{
    private readonly McDatabaseContext ctx;
    public OrderRepository(McDatabaseContext ctx)
        => this.ctx = ctx;

     public async Task<int> CreateORder(int storeId)
    {

        var selectedStore = 
            from store in ctx.Stores
            where store.Id == storeId
            select store;

        if (selectedStore.Any())
            throw new Exception("store doesn't exist");

        var clientOrder = new ClientOrder();
        clientOrder.StoreId = storeId;
        clientOrder.OrderCode = "abcd1234";

        ctx.Add(clientOrder);
        await ctx.SaveChangesAsync();

         return clientOrder.Id;
    }

    public async Task CancelOrder(int orderId)
    {
        var currentOrder = await getOrder(orderId);
        if (currentOrder is null)
            throw new Exception("The order desn't exist.");
        
        ctx.Remove(currentOrder);
        await ctx.SaveChangesAsync();
    }

    public async Task AddItem(int orderId, int productId)
    {
        var order = await getOrder(orderId);
        if (order is null)
            throw new Exception("Order doesn't exist.");
        
        var products =
            from product in ctx.Products
            where product.Id == productId
            select product;
        var selectedProduct = await products
            .FirstOrDefaultAsync();
        if (selectedProduct is null)
            throw new Exception("Product doesn't exist.");
        
        var item = new ClientOrderItem();
        item.ClientOrderId = orderId;
        item.ProductId = productId;

        ctx.Add(item);
        await ctx.SaveChangesAsync();
    }


    public Task DeliveryOrder(int orderId)
    {
        throw new System.NotImplementedException();
    }

    public Task FinishOrder(int orderId)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Product>> GetMenu(int orderId)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Product>> GetOrderItems(int orderId)
    {
        throw new System.NotImplementedException();
    }

    public async Task RemoveItem(int orderId, int itemId)
    {
        var items =
            from item in ctx.Products
            where item.Id == itemId
            select item;
        var itemSelected = items.FirstOrDefaultAsync(); 
        if (itemSelected is null)
            throw new Exception("Product doesn't exist.");

        var order = getOrder(orderId);
        if (order is null)
            throw new Exception("Order doesn't exist.");

        var itemOrders = 
            from itemOrder in ctx.ClientOrderItems
            where itemOrder.Id == orderId
            select itemOrder;
        var itemOrderSelected = itemOrders.FirstOrDefaultAsync();

        ctx.Remove(itemOrderSelected);

        await ctx.SaveChangesAsync();

    }

    private async Task<ClientOrder> getOrder(int orderId)
    {
           var orders =
            from order in ctx.ClientOrders
            where order.Id == orderId
            select order;
        
        return await orders.FirstOrDefaultAsync();
    }
}
