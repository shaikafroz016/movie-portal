using eticket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext db { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            db = context;
        }

        //setting up seesion in order to inject the shopping cart dependencie injection
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = db.shoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                db.shoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            db.SaveChanges();
        }
        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = db.shoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    db.shoppingCartItems.Remove(shoppingCartItem);
                }
            }
            db.SaveChanges();
        }


        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return shoppingCartItems ?? (shoppingCartItems = db.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }
        public double GetShoppingCartTotal()
        {
            var total = db.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.price * n.Amount).Sum();
            return total;
        }
        public async Task ClearShoppingCartAsync()
        {
            var items = await db.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            db.shoppingCartItems.RemoveRange(items);
            await db.SaveChangesAsync();
        }
    }
}
