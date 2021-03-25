using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFPE_AuthorisationMicroservice.Models;
using MFPE_AuthorisationMicroservice.ProviderLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MFPE_AuthorisationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        //static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private readonly IUserProvider _userProvider;

        public static string TokenString;

        public AuthController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserDetails login)
        {
            try
            {
                //_log4net.Info("Authentication initiated for " + login.Username);
                IActionResult response = Unauthorized();
                string tokenString = _userProvider.LoginProvider(login);
                // HttpContext.Session.SetString("tokenString", tokenString);
                TokenString = tokenString;
                int uid = _userProvider.GetUserId(login);
                if (tokenString == null)
                {
                    //_log4net.Error("Login failed for " + login.Username);

                    return NotFound();
                }
                else
                {
                    //HttpContext.Session.SetString("token",tokenString);
                    return Ok(new { token = tokenString, User_Id = uid });
                }
            }
            catch (Exception e)
            {
                //_log4net.Error("Error in login for user " + login.Username + " as " + e.Message);
                return new StatusCodeResult(500);
            }
        }


        [HttpGet]
        public TokenPasser GetToken()
        {
            TokenPasser tokenPasser = new TokenPasser() { id = 1, mytoken = TokenString };
            return tokenPasser;
        }

    }
}