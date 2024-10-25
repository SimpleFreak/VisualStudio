using Microsoft.EntityFrameworkCore;
using PensionFund.Core.Abstractions;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionFund.DataAccess.Repositories
{
    public class UserRepository(PensionFundDbContext context) : IUserRepository
    {
        private readonly PensionFundDbContext _context = context;

        public async Task<List<User>> Get()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            var users = userEntities
                .Select(user => User
                    .Create(user.Id, user.Login, user.Password, user.Role).User)
                .ToList();

            return users;
        }

        public async Task<User> GetByLogin(string login)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == login) ?? throw new Exception();

            return userEntity;
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = User.Create(user.Id, user.Login, user.Password, user.Role);

            await _context.Users.AddAsync(userEntity.User);
            await _context.SaveChangesAsync();

            return userEntity.User.Id;
        }
        
        public async Task<Guid> Update(Guid id, string login, string password, string role)
        {
            await _context.Users
                .Where(user => user.Id == id)
                .ExecuteUpdateAsync(set => set
                .SetProperty(user => user.Login, user => login)
                .SetProperty(user => user.Password, user => password)
                .SetProperty(user => user.Role, user => role));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Persons
                .Where(user => user.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
