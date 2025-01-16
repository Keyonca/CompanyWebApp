using CompanyWebApp.Domain;
using CompanyWebApp.Domain.Entities;
using CompanyWebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWebApp.Models.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly DataManager _dataManager;

        public MenuViewComponent(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Service> list = await _dataManager.Services.GetServicesAsync();

            //Доменную сущность на клиенте использовать не рекомендуется, оборачиваем ее в DTO
            IEnumerable<ServiceDTO> listDTO = HelperDTO.TransformServices(list);

            return await Task.FromResult((IViewComponentResult)View("Default", listDTO));
        }
    }
}
