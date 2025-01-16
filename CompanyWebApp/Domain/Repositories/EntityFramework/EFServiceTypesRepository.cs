using CompanyWebApp.Domain.Entities;
using CompanyWebApp.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebApp.Domain.Repositories.EntityFramework
{
    public class EFServiceTypesRepository : IServiceTypesRepository
    {
        private readonly AppDbContext _context;

        public EFServiceTypesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceType>> GetServiceTypesAsync()
        {
            return await _context.ServiceTypes.Include(x => x.Services).ToListAsync();
        }

        public async Task<ServiceType?> GetServiceTypeByIdAsync(int id)
        {
            return await _context.ServiceTypes.Include(x => x.Services).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveServiceTypeAsync(ServiceType entity)
        {
            _context.Entry(entity).State = entity.Id == default ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceTypeAsync(int id)
        {
            _context.Entry(new ServiceType() { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
