using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SurveyApi.Data;
using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyApi.Services.AuthService
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthRepo(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
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

        public async Task<bool> UserIdExist(Guid id)
        {
            if (await _context.User.AnyAsync(u => u.IdUser.ToString().ToUpper() == id.ToString().ToUpper()))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UserExist(string username, string email)
        {
            var user = await _context.User
                .Where(e => e.Email.ToLower() == email.ToLower())
                .AnyAsync(u => u.StrName.ToLower() == username.ToLower());

            if (user)
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

            //Parameters that will have the Token
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

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUser()
        {
            var response = new ServiceResponse<List<GetUserDto>>();

            var users = await _context.User.ToListAsync();

            response.Data = users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();

            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(Guid id)
        {
            ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>>();
            try
            {
                User user = await _context.User
                    .FirstOrDefaultAsync(u => u.IdUser.ToString().ToUpper() == id.ToString().ToUpper());

                if (user != null)
                {
                    _context.User.Remove(user);
                    await _context.SaveChangesAsync();

                    response.Data = _context.User.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "User Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(Guid id)
        {
            var response = new ServiceResponse<GetUserDto>();
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.IdUser.ToString().ToUpper() == id.ToString().ToUpper());

            if (user != null)
            {
                response.Data = _mapper.Map<GetUserDto>(user);
            }
            else
            {
                response.Success = false;
                response.Message = "User Not Found";
            }


            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(User user, string password, Guid id)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();

            try
            {
                if(await UserIdExist(id))
                {
                    CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.IdUser = id;

                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetUserDto>(user);
                }
                else
                {
                    response.Success = false;
                    response.Message = "User Not Found";
                }
            }
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
