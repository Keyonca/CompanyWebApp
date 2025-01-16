namespace CompanyWebApp.Domain.Entities
{
    public class ServiceType : EntityBase
    {
        public ICollection<Service>? Services { get; set; }
    }
}
