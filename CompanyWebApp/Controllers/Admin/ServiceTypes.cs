using CompanyWebApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWebApp.Controllers.Admin
{
    public partial class AdminController
    {
        public async Task<IActionResult> ServiceTypesEdit(int id)
        {
            //В зависимости от наличия ID либо добавляем, либо изменяем запись
            ServiceType? entity = id == default
                ? new ServiceType()
                : await _dataManager.ServiceTypes.GetServiceTypeByIdAsync(id);

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> ServiceTypesEdit(ServiceType entity)
        {
            //В модели присутствуют ошибка, возвращаем на доработку
            if (!ModelState.IsValid)
                return View(entity);

            await _dataManager.ServiceTypes.SaveServiceTypeAsync(entity);
            _logger.LogInformation($"Добавлен/обновлен тип услуги с ID: {entity.Id}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ServiceTypesDelete(int id)
        {
            //Так как в целях безопасности отключено каскадное удаление, то прежде чем удалить категорию,
            //нужно убедиться, что на нее нет ссылки ни у одной из услуг
            await _dataManager.ServiceTypes.DeleteServiceTypeAsync(id);
            _logger.LogInformation($"Удален тип услуги с ID: {id}");

            return RedirectToAction("Index");
        }
    }
}
