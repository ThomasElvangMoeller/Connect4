using Connect4.Data;
using Connect4.Models;
using Connect4.Models.ApiRequest;
using Connect4.Models.ApiResponse;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Services
{
    public class UserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserStorage _userContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IOptions<AppSettings> appSettings, IUserStorage context, UserManager<ApplicationUser> userManager)
        {
            _appSettings = appSettings.Value;
            _userContext = context;
            _userManager = userManager;
        }

        public async Task<UserResponse> AuthenticateAsync(UserRequest model)
        {
            ApplicationUser user = _userContext.Users.SingleOrDefault(q => q.UserName == model.Username);
            if(user != null)
            {
                if( await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    // authentication successful so generate jwt token
                    string token = GenerateJwtToken(user);
                    return new UserResponse(user, token);
                }
            }

            // return null if user not found
            return null;
        }
        public ApplicationUser GetUser(Guid userId)
        {
            return _userContext.Users.FirstOrDefault(q => q.Id == userId);
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await _userManager.GetUserAsync(claimsPrincipal);
        }

        public async Task<UserResult> CreateUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return new UserResult(error: "Password is required" );
            if (_userContext.Users.Any(q => q.UserName == username)) return new UserResult(error: $"Could not create user. Username '{username}' is already taken" );
            ApplicationUser user = new ApplicationUser() { UserName = username };
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                ApplicationUser resultUser = _userContext.Users.SingleOrDefault(q => q.UserName == username);
                return new UserResult(user: resultUser);
            }
            else
            {
                return new UserResult(error: result.Errors.First().Description); //TODO: Refactor this, is bad
            }
        }


        // helper methods -------------------------------------------------------------------------------------

        private string GenerateJwtToken(ApplicationUser user)
        {
            // generate token that is valid for 7 days
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
