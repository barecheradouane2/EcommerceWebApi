using EcommerceWeb.Api.Models.DTO;
using EcommerceWeb.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase


    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;


        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        { 
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        // post api/Auth/register

        [HttpPost]

        [Route("register")]

        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var user = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };
            var result = await userManager.CreateAsync(user, registerRequestDTO.Password);
            if (result.Succeeded)
            {
               if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Length > 0) {
                    result = await userManager.AddToRolesAsync(user, registerRequestDTO.Roles);

                    if (result.Succeeded)
                    {
                        return Ok("User Register plz log in");
                    }

                }
               
            }
            return BadRequest(result.Errors);
        }

        [HttpPost]

        [Route("login")]

        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, loginRequestDTO.Password))
            {
                var roles = await userManager.GetRolesAsync(user);
                // create token 
                if (roles!=null){
                   var jwtToken=tokenRepository.CreateJWTToken(user, roles.ToList());

                    var response = new LoginResponseDTO
                    {

                        JwtToken = jwtToken
                    };
                    return Ok(response);
                }
             
              
            }
            return BadRequest("Invalid login attempt");
        }


    }
}
