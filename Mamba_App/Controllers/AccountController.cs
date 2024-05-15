using Mamba.Core.Models;
using Mamba_App.DTOs.AccountDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mamba_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            User user = new User()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName,

            };
            var result=await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View();
            }
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user =await _userManager.FindByNameAsync(loginDto.EmailOrUsername);
            if (user == null)
            {
                user=await _userManager.FindByEmailAsync(loginDto.EmailOrUsername);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email/Username ve ya password yanlisdir!!!");
                    return View();
                }
            }
            var result=await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("","Birazdan yeniden sina") ;
                return View();
            }
            if(!result.Succeeded) 
            {
                ModelState.AddModelError("", "Email/Usernema ve ya password yanlisdir");
                return View();
            }
            _signInManager.SignInAsync(user, loginDto.IsRemember);
            
            return RedirectToAction("Index","Home");
        }
    }
}
