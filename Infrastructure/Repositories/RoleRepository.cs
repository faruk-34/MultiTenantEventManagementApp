using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Insert(Role role, CancellationToken cancellationToken)
        {
            await _context.Roles.AddAsync(role,cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Where(r => r.Id == id && !r.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Role>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task Update(Role role, CancellationToken cancellationToken)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }

}
