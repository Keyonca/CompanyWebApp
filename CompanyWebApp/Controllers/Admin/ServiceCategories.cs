using CompanyWebApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWebApp.Controllers.Admin
{
    public partial class AdminController
    {
        public async Task<IActionResult> ServiceCategoriesEdit(int id)
        {
            //В зависимости от наличия ID либо добавляем, либо изменяем запись
            ServiceCategory? entity = id == default 
                ? new ServiceCategory() 
                : await _dataManager.ServiceCategories.GetServiceCategoryByIdAsync(id);

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> ServiceCategoriesEdit(ServiceCategory entity)
        {
            //В модели присутствуют ошибка, возвращаем на доработку
            if (!ModelState.IsValid)
                return View(entity);

            await _dataManager.ServiceCategories.SaveServiceCategoryAsync(entity);
            _logger.LogInformation($"Добавлена/обновлена категория услуги с ID: {entity.Id}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ServiceCategoriesDelete(int id)
        {
            //Так как в целях безопасности отключено каскадное удаление, то прежде чем удалить категорию,
            //нужно убедиться, что на нее нет ссылки ни у одной из услуг
            await _dataManager.ServiceCategories.DeleteServiceCategoryAsync(id);
            _logger.LogInformation($"Удалена категория услуги с ID: {id}");

            return RedirectToAction("Index");
        }
    } 
}
