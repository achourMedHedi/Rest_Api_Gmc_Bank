using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GmcBankApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GmcBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private UserManager<IdentityUser> _userManager;
        public AuthController(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            this._configuration = configuration;
            this._userManager = userManager;
        }
        public string GenerateToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id),

            };
            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Auth:Secret"]));
            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _configuration["Auth:Issuer"],
                audience: _configuration["Auth:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }


        //[HttpPost("/api/auth/login")]
        //public IActionResult Login([FromBody] LoginModel model)
        //{
        //    //var user = new IdentityUser
        //    //{
        //    //    Email = model.Email,
        //    //    UserName = model.Email
        //    //};
        //    //await _userManager.CreateAsync(user, model.Password);
        //    //var user = await _userManager.FindByEmailAsync(model.Email);
        //    //if (user != null)
        //    //{
        //    //    //var t = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    //    //await _userManager.ResetPasswordAsync(user, t, model.Password);

        //    //    var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
        //    //    if (checkPassword)
        //    //    {
        //    //        return Ok(GenerateToken(user));
        //    //    }
        //    //}

        //    return Ok(GenerateToken(model));
        //}



    }
}