using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestFull_Api.Models.Domains.DTO;
using RestFull_Api.Reposotry;

namespace RestFull_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<IdentityUser> userManager;
        public readonly ITokenCreator tokenCreator;


        public AuthController(UserManager<IdentityUser> userManager,ITokenCreator tokenCreator)
        {
            this.userManager = userManager;
            this.tokenCreator = tokenCreator;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)

         
        {   if (  !ModelState.IsValid)
            {   
             string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest("errors are"+messages);
            }
            
                        var identityUser = new IdentityUser
                {
                    UserName=registerDTO.Username,
                    Email=registerDTO.Username,
                    
                };
           var res= await userManager.CreateAsync(identityUser,registerDTO.Password);

            if (res.Succeeded)
            {
                // Add Role to this User
                if (registerDTO.Roles != null && registerDTO.Roles.Any())
                    res = await userManager.AddToRolesAsync(identityUser, registerDTO.Roles);

                if (res.Succeeded)
                {
                    return Ok("Please Login");
                }

            }
            return BadRequest("bye");
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult>login([FromBody] loginRequestDto loginRequest)
        {       //Find User By Email
            var User=await userManager.FindByEmailAsync(loginRequest.Username);

            if (User == null)
                return BadRequest();
            else
            { // check Password
              var res= await userManager.CheckPasswordAsync(User, loginRequest.Password);

                if(res==true)
                {   // Get Roles for this User
                    var roles=await userManager.GetRolesAsync(User);

                    if (roles != null)
                    { // create Token
                      var jwtToken=  tokenCreator.CreateJWTToken(User, roles.ToList());
                      var response = new LoginResponseDto { JwtToken = jwtToken };
                        return Ok(response);
                    } 

                }
            }
            return BadRequest();
        }
    }
}
