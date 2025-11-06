using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        /// <summary>
        /// Get a user by email.
        /// </summary>
        Task<UserInfo?> GetByEmailAsync(string email);

        IEnumerable<UserInfo> GetAll();

        /// <summary>
        /// Check if a user with the given email already exists.
        /// </summary>
        Task<bool> ExistsByEmailAsync(string email);

        /// <summary>
        /// Authenticate a user by matching email and password.
        /// </summary>
        Task<UserInfo?> AuthenticateAsync(string email, string password);

        /// <summary>
        /// Add a new user asynchronously.
        /// </summary>
        Task AddAsync(UserInfo entity); // ✅ Added this
    }
}
