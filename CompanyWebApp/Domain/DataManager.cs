using CompanyWebApp.Domain.Repositories.Abstract;

namespace CompanyWebApp.Domain
{
    public class DataManager
    {
        public IServiceCategoriesRepository ServiceCategories {  get; set; }
        public IServicesRepository Services { get; set; }
        public IServiceTypesRepository ServiceTypes { get; set; }

        public DataManager(IServiceCategoriesRepository serviceCategoriesRepository, IServicesRepository servicesRepository, IServiceTypesRepository serviceTypesRepository)
        {
            ServiceCategories = serviceCategoriesRepository;
            Services = servicesRepository;
            ServiceTypes = serviceTypesRepository;
        }
    }
}
