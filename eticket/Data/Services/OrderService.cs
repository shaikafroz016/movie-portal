using eticket.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext db;
        public OrderService(AppDbContext context)
        {
            db = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await db.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Where(n => n.userId == userId).ToListAsync();
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                userId = userId,
                Email = userEmailAddress
            };
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    price = item.Movie.price
                };
                await db.OrderItems.AddAsync(orderItem);
            }
            await db.SaveChangesAsync();
        }
    }
}
