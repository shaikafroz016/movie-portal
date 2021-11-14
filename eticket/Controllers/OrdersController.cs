using eticket.Data.Cart;
using eticket.Data.Services;
using eticket.Data.ViewModel;
using eticket.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService dalmovie;
        private readonly ShoppingCart dalshoppingcart;
        private readonly IOrderService dalordderservice;

        public OrdersController(IMoviesService movieservixe,ShoppingCart shoppingcartservice ,IOrderService orderService)
        {
            dalmovie = movieservixe;
            dalshoppingcart = shoppingcartservice;
            dalordderservice = orderService;
        }
        public async Task<IActionResult> Index()
        {
            //Taking userid and email from loggedin user and saving into orders
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole=User.FindFirstValue(ClaimTypes.Role);
            var orders = await dalordderservice.GetOrdersByUserIdAndRoleAsync(userId,userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }
            var items = dalshoppingcart.GetShoppingCartItems();
            dalshoppingcart.shoppingCartItems = items;
            var response = new ShoppingcartVM()
            {
                ShoppingCart = dalshoppingcart,
                ShoppingCartTotal = dalshoppingcart.GetShoppingCartTotal()
            };


            return View(response);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await dalmovie.GetbyId(id);

            if (item != null)
            {
                dalshoppingcart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart), new { message = "Added" });
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await dalmovie.GetbyId(id);

            if (item != null)
            {
                dalshoppingcart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = dalshoppingcart.GetShoppingCartItems();

            //Taking userid and email from loggedin user and saving into orders
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await dalordderservice.StoreOrderAsync(items, userId, userEmailAddress);
            await dalshoppingcart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

    }
}
