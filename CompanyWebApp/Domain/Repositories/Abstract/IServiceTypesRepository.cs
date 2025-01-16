using CompanyWebApp.Domain.Entities;

namespace CompanyWebApp.Domain.Repositories.Abstract
{
    public interface IServiceTypesRepository
    {
        Task<IEnumerable<ServiceType>> GetServiceTypesAsync();
        Task<ServiceType?> GetServiceTypeByIdAsync(int id);
        Task SaveServiceTypeAsync(ServiceType entity);
        Task DeleteServiceTypeAsync(int id);
    }
}
