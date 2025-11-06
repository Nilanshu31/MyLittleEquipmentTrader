using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Domain.Repositories;
using MyLittleEquipmentTrader.Infrastructure.Data;
using MyLittleEquipmentTrader.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // Generic repositories
        public IRepository<Product> Products { get; }
        public IRepository<ProductAttribute> ProductAttributes { get; }
        public IRepository<UserInfo> UserInfos { get; }

        // Specialized repositories (lazy-loaded)
        private IManufacturerRepository _manufacturerRepository;
        private IPlanRepository _planRepository;
        private ISubscriptionRepository _subscriptionRepository;
        private IReportRepository _reportRepository;
        private ISalesOrderRepository _salesOrderRepository;
        private ICategoryRepository _categoryRepository;
        private IUserInfoRepository _userInfoRepository;

        public IAccessRoleRepository AccessRoles { get; }
        public ITenantRepository Tenants { get; }

        // Lazy-loaded property accessors
        public IManufacturerRepository Manufacturers =>
            _manufacturerRepository ??= new ManufacturerRepository(_context);

        public IPlanRepository Plans =>
            _planRepository ??= new PlanRepository(_context);

        public ICategoryRepository Categories =>
            _categoryRepository ??= new CategoryRepository(_context);

        public ISubscriptionRepository Subscriptions =>
            _subscriptionRepository ??= new SubscriptionRepository(_context);

        public IReportRepository Reports =>
            _reportRepository ??= new ReportRepository(_context);

        public ISalesOrderRepository SalesOrders =>
            _salesOrderRepository ??= new SalesOrderRepository(_context);

        public IUserInfoRepository UserInfoRepository =>
            _userInfoRepository ??= new UserInfoRepository(_context);

        // Constructor
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // Instantiate generic repositories
            Products = new ProductRepository(context);
            ProductAttributes = new ProductAttributeRepository(context);
            UserInfos = new Repository<UserInfo>(context); // generic repo
            Tenants = new TenantRepository(context);
            AccessRoles = new AccessRoleRepository(context);
        }

        // Commit changes to the database
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Rollback placeholder
        public async Task RollbackAsync()
        {
            await Task.CompletedTask;
        }

        // Dispose method to clean up resources
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
