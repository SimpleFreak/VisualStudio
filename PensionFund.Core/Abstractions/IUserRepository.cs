using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<List<User>> Get();
        Task<Guid> Create(User user);
        Task<User> GetByLogin(string login);
        Task<Guid> Update(Guid id, string login, string password, string role);
        Task<Guid> Delete(Guid id);
    }
}
