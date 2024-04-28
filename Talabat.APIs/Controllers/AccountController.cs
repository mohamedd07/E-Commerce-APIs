﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Controllers
{
    public class AccountController: BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
              return Unauthorized(new APIResponse(401));
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if(!result.Succeeded)
                return Unauthorized(new APIResponse(401));

            return Ok(new UserDto()
            {
                displayName = user.displayName,
                Email = user.Email,
                Token = "My Token"

            }) ;

        }

        [HttpPost("register")]
        public  async Task<ActionResult<UserDto>> Register (RegisterDto model)
        {
            var user = new AppUser()
            {
                displayName = model.displayName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.PhoneNumber,


            };
        
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return Unauthorized(new APIResponse(401));

            return Ok(new UserDto()
            {
               displayName = user.displayName,
                Email = user.Email,
                Token = "My Token"

            });

        }
    }
}
