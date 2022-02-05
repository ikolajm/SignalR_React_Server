using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using backend.Services.AuthService;
using backend.Services.UserService;
using backend.Models;

namespace backend.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AuthController(IConfiguration configuration, IUserService userService, IAuthService authService)
        {
            _configuration = configuration;
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserForRegister request)
        {

            // == PRE REGISTER CHECKS == 
            // Make sure no given fields are blank
            if (
                request.Username == "" ||
                request.Email == "" ||
                request.Password == "" ||
                request.ConfirmPassword == ""
            )
            {
                return BadRequest("Make sure all fields are filled in.");
            }
            // Check that email is not taken
            if (await _userService.EmailExists(request.Email.ToLower()))
            {
                return BadRequest("Email is already in use");
            }
            // Check that username is not taken
            if (await _userService.UserExists(request.Username.ToLower()))
            {
                return BadRequest("Username is already taken");
            }
            // Check that Password and ConfirmPassword match
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Make sure both password fields match.");
            }
            
            // == USER CREATION ==
            var user = new User();
            user.Email = request.Email;
            user.Username = request.Username;
            // Create password hash + salt
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            // Assign the default avatar to the Model
            user.Avatar = "";

            var created = await _authService.RegisterUser(user);

            // Create a token for the user
            string token = CreateToken(user);

            return Ok(new { token = token, newUser = created });
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserForLogin request)
        {
            // == PRE LOGIN CHECKS == 
            // Make sure no given fields are blank
            if (
                request.Email == "" ||
                request.Password == ""
            )
            {
                return BadRequest("Make sure all fields are filled in.");
            }
            // Check that password is what is listed for the given email
            var user = await _authService.GetUser(request.Email);
            // If user not found with that email
            if (user == null)
            {
                return BadRequest("User not found with that email");
            }
            // If password incorrect for user 
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Incorrect password for given email");
            }

            // Generate token, return user
            string token = CreateToken(user);
            return Ok(new { token = token, user = user });
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}