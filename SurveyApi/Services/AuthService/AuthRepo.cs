using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SurveyApi.Data;
using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyApi.Services.AuthService
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepo(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password, string email)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.User
                .Where(e => e.Email.ToLower().Equals(email.ToLower()))
                .FirstOrDefaultAsync(u => u.StrName.ToLower().Equals(username.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User Not Found";

            }
            else if (!verifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong Password";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<string>> Register(User user, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (await UserExist(user.StrName, user.Email))
            {
                response.Success = false;
                response.Message = "The User Already Exist!";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.User.Add(user);

            await _context.SaveChangesAsync();
            response.Data = user.IdUser.ToString();

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(User user, string password, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserIdExist(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExist(string username, string email)
        {
            var userName = await _context.User.AnyAsync(u => u.StrName.ToLower() == username.ToLower());
            var userEmail = await _context.User.AnyAsync(u => u.Email.ToLower() == email.ToLower());

            if (userName && userEmail)
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {

            //Listado de Parametros que tendrá el Json Token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.StrName),
                new Claim(ClaimTypes.Surname, user.StrFirstSurname),
                new Claim(ClaimTypes.Email, user.Email),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDesc);

            return tokenHandler.WriteToken(token);
        }

    }
}
