using PensionFund.Application.Interfaces;
using PensionFund.Core.Abstractions;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PensionFund.Application.Interfaces.Auth;

namespace PensionFund.Application.Services
{
    public class UserService(IPasswordHasher passwordHasher,
        IUserRepository usersRepository, IJwtProvider jwtProvider) : IUserService
    {
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUserRepository _usersRepository = usersRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task Register(string login, string password, string role)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var (user, error) = User.Create(Guid.NewGuid(), login, hashedPassword, role);

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }

            await _usersRepository.Create(user);
        }

        public async Task<string> Login(string login, string password)
        {
            var user = await _usersRepository.GetByLogin(login);

            var result = _passwordHasher.Verify(password, user.Password);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.Get();
        }

        public async Task<Guid> CreateUser(User user)
        {
            return await _usersRepository.Create(user);
        }

        public async Task<Guid> UpdateUser(Guid id, string login, string password, string role)
        {
            return await _usersRepository.Update(id, login, password, role);
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _usersRepository.Delete(id);
        }
    }
}
