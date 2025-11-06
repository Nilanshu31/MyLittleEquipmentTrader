using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Domain.Repositories;
using MyLittleEquipmentTrader.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<ProductAttribute> ProductAttributes { get; }
        IManufacturerRepository Manufacturers { get; }
        IAccessRoleRepository AccessRoles { get; }
        IRepository<UserInfo> UserInfos { get; }
        IReportRepository Reports { get; }
        ITenantRepository Tenants { get; }
        IPlanRepository Plans { get; }

        ICategoryRepository Categories { get; }
        IUserInfoRepository UserInfoRepository { get; }
        ISubscriptionRepository Subscriptions { get; }
        ISalesOrderRepository SalesOrders { get; }

        Task<int> CommitAsync();
        Task RollbackAsync();
    }
}
