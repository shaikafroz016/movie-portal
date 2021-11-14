using eticket.Data;
using eticket.Data.Static;
using eticket.Data.ViewModel;
using eticket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Users()
        {
            var result = _context.Users.ToList();
            return View(result);
        }
        [Authorize]
        public async Task<IActionResult> UpdateProfile()
        {
            var currentUser = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var userdetails = new RegisterVM()
            {
                FullName = currentUser.Fullname,
                EmailAddress = currentUser.Email,
                Username = currentUser.UserName
            };

            return View(userdetails);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                Fullname = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.Username
            };
            var newUserResponse = await _userManager.UpdateAsync(newUser);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);


            return Content("Update succes");
        }
        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM values)
        {
            if (!ModelState.IsValid)
            {
                return View(values);
            }
            var user = await _userManager.FindByEmailAsync(values.EmailAddress);
            if (user != null)
            {
                var passwordcheck =await _userManager.CheckPasswordAsync(user,values.Password);
                if (passwordcheck)
                {
                    var result =await _signInManager.PasswordSignInAsync(user, values.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                else
                {
                    TempData["Error"] = "Wrong Password...Please try again";
                    return View(values);
                }

                
            }
            TempData["Error"] = "Wrong Credentials...Please try again";
            return View(values);
        }

        public IActionResult Register()
        {
            return View(new RegisterVM());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                Fullname = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
           

            return View("RegisterCompleted");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
