using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public UserInfoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserInfo?> GetByEmailAsync(string email)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(u => u.Email == email);
        }

        public IEnumerable<UserInfo> GetAll()
        {
            return _context.UserInfo.ToList();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.UserInfo.AnyAsync(u => u.Email == email);
        }

        public async Task<UserInfo?> AuthenticateAsync(string email, string password)
        {
            return await _context.UserInfo
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }

        // ✅ This comes from IRepository<UserInfo>, but we explicitly include it for clarity
        public new async Task AddAsync(UserInfo entity)
        {
            await base.AddAsync(entity);
        }
    }
}
