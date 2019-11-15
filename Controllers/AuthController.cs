using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BigOferta.API.Dtos;
using BigOferta.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BigOferta.API.Data;

namespace BigOferta.API.Controllers
{
    [Route("bowebapi/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DatingRepository _repo;

        public AuthController(IConfiguration config, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager,
            DatingRepository repo)
        {
            this._repo = repo;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._mapper = mapper;
            this._config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            User userToCreate = _mapper.Map<User>(userForRegisterDto);
            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                UserForReturnDto userToReturn = _mapper.Map<UserForReturnDto>(userToCreate);

                return Ok(userToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            User userFromStore = await _userManager.FindByNameAsync(userForLoginDto.UserName);

            if (userFromStore == null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(userFromStore, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserForReturnDto>(userFromStore);

                return Ok(new
                {
                    token = GenerateJwtToken(userFromStore),
                    user = userToReturn
                });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config
                .GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string serializedToken = tokenHandler.WriteToken(token);

            return serializedToken;
        }

        [HttpPut("{userId}/update")]
        [Authorize]
        public async Task<IActionResult> UpdateAccountUser(int userId, UserForUpdateDto userForUpdateDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            User user = await _repo.GetUser(userId);
            
            _mapper.Map<UserForUpdateDto, User>(userForUpdateDto, user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (userForUpdateDto.CurrentPassword != null &&
                userForUpdateDto.CurrentPassword.Trim().Length > 0 &&
                userForUpdateDto.NewPassword != null &&
                userForUpdateDto.NewPassword.Trim().Length > 0)
            {
                result = await _userManager.ChangePasswordAsync(user, 
                    userForUpdateDto.CurrentPassword, userForUpdateDto.NewPassword);
                
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            UserForReturnDto userForReturnDto = _mapper.Map<UserForReturnDto>(user);

            return Ok(userForReturnDto);
        }

    }
}