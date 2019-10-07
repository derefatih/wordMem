using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WordMem.Business.Abstract;
using WordMem.CrossCutting.DTOs;

namespace WordMem.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthService authService;
        private IUserService userService;


        public AuthController(IAuthService _authService, IUserService _userService)
        {
            authService = _authService;
            userService = _userService;
        }


        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO model)
        {

            if (ModelState.IsValid)
            {
                var result = await authService.LogginUser(model);


                if (result.Succeeded)
                {
                    var user =  await userService.GetUserByEmail(model.Email);

                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>{
                            new Claim(ClaimTypes.Email,user.Email)
                        },
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString, id = user.Id, username = user.UserName});
                }
                else
                {
                    return BadRequest("kullanıcı adı yada sifre hatalı");
                }

            }
            return ValidationProblem();







            //if (user == null)
            //{
            //    return BadRequest("Invalid client request");
            //}

            //if (user.Email == "fatih@gmail.com" && user.Password == "1234Qwer*")
            //{
            //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            //    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //    var tokeOptions = new JwtSecurityToken(
            //        issuer: "http://localhost:5000",
            //        audience: "http://localhost:5000",
            //        claims: new List<Claim>(),
            //        expires: DateTime.Now.AddMinutes(5),
            //        signingCredentials: signinCredentials
            //    );

            //    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            //    return Ok(new { Token = tokenString });
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }
    }
}
