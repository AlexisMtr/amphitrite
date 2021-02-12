using Amphitrite.Configuration;
using Amphitrite.Dtos;
using Amphitrite.Exceptions;
using Amphitrite.Models;
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

namespace Amphitrite.Services
{
    public class UserService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IssuerSigningKeySettings signingKeySettings;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager, IOptions<IssuerSigningKeySettings> signingKeySettings)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.signingKeySettings = signingKeySettings.Value;
        }

        public async Task<string> Login(Credentials credentials)
        {
            User user = await userManager.FindByEmailAsync(credentials.Email);
            if (user == null) throw new UnauthorizedAccessException();

            IList<Claim> claims = await userManager.GetClaimsAsync(user);
            IList<string> userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                Claim roleClaim = new Claim(ClaimTypes.Role, userRoles.First());
                claims.Add(roleClaim);
            }
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var checkPassword = await signInManager.CheckPasswordSignInAsync(user, credentials.Password, false);
            if (!checkPassword.Succeeded) throw new InvalidCredentialsException("Email or Password incorrect");

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKeySettings.SigningKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> SignIn(UserCreationDto model)
        {
            IdentityResult userResult = await userManager.CreateAsync(new User
            {
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper()
            }, model.Password);

            if (!userResult.Succeeded) throw new SignInException("Error while registring the user");

            User user = await userManager.FindByEmailAsync(model.Email);
            IdentityResult roleResult = await userManager.AddToRoleAsync(user, model.Role);

            if (!roleResult.Succeeded) throw new SignInException("Error on assigning role to the user");

            return await Login(new Credentials { Email = model.Email, Password = model.Password });
        }
    }
}
