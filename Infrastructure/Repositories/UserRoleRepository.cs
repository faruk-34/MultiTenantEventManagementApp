using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertUserRole(UserRole userRole,CancellationToken cancellationToken)
        {
            await _context.UserRoles.AddAsync(userRole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
           
        }

        public async Task<List<UserRole>> GetUserRolesByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task  UpdateUserRole(UserRole userRole, CancellationToken cancellationToken)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
           
        }

        public async Task<UserRole> GetUserRole(int Id,CancellationToken cancellation)
        {
            return await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.Id == Id, cancellation);

 
        }

        public async Task  DeleteUserRole(UserRole userRole,CancellationToken cancellationToken)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole), "User role bulunamadı");

            var existingUserRole = await _context.UserRoles.FindAsync(userRole.Id);

            if (existingUserRole == null)
                throw new KeyNotFoundException("User role bulunamadı");

            _context.UserRoles.Remove(existingUserRole);
            await _context.SaveChangesAsync();

       
        }


    }
}


 