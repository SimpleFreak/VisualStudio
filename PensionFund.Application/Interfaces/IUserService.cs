using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string login, string password);

        Task Register(string login, string password, string role);

        Task<List<User>> GetAllUsers();

        Task<Guid> CreateUser(User user);

        Task<Guid> UpdateUser(Guid id, string login, string password, string role);

        Task<Guid> DeleteUser(Guid id);
    }
}