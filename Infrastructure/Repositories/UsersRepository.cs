
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Insert(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Users?> Get(int id,CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(id,cancellationToken);
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<IEnumerable<Users>> GetAllByTenantAsync(int tenantId,CancellationToken cancellationToken)
        {
            return await _context.Users.Where(u => u.TenantId == tenantId).ToListAsync();
        }

        public async Task Update(Users user,CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
 
    }

}
