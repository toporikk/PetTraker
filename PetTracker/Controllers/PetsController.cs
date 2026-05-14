using Microsoft.AspNetCore.Mvc;
using PetTracker.Models;
using PetTracker.Services;
using PetTracker.ViewModels;
using System.Linq;

namespace PetTracker.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        //Контроллер вызывает метод GetAllPets() у сервиса
        //Получает список питомцев
        //Возвращает представление Index.cshtml с передачей списка
        public IActionResult Index()
        {
            var pets = _petService.GetAllPets();
            return View(pets);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _petService.AddPet(viewModel);
                ViewBag.Message = "Питомец добавлен!";
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public IActionResult Age(int id)
        {
            var pet = _petService.GetPetById(id);
            if (pet == null)
                return NotFound();
            return View(pet);
        }

        public IActionResult ByType(string type)
        {
            var all = _petService.GetAllPets();

            List<Pet> filtered = new List<Pet>();

            if (type == "Кот")
            {
                filtered = all.Where(p => p.Type == "Кот" || p.Type == "кот").ToList();
            }
            else if (type == "Собака")
            {
                filtered = all.Where(p => p.Type == "Собака" || p.Type == "собака").ToList();
            }
            else
            {
                filtered = all;
            }

            return View("Index", filtered);
        }

        // GET: /Pets/Edit/{id}  - ПОКАЗАТЬ ФОРМУ
        public IActionResult Edit(int id)
        {
            // 1. Просим сервис найти питомца по ID
            var pet = _petService.GetPetById(id);
            // 2.Если не найден -показываем ошибку 404
            if (pet == null)
                return NotFound();
            // 3. Передаём найденного питомца в представление
            return View(pet);
        }

        // POST: /Pets/Edit - СОХРАНИТЬ ИЗМЕНЕНИЯ
        [HttpPost] //этот метод вызывается только на POST-запросы
        public IActionResult Edit(Pet pet)
        {
            // Проверяем, все ли поля заполнены правильно
            if (ModelState.IsValid)
            {
                // Вызываем сервис для обновления
                _petService.UpdatePet(pet);
                // Сохраняем сообщение об успехе (покажется на странице списка)
                TempData["Message"] = "Данные питомца обновлены!";
                // Перенаправляем на страницу со списком
                return RedirectToAction("Index"); ///Браузер получает команду перейти на / Pets. Срабатывает метод Index() контроллера, который показывает обновлённый список.
            }
            // Если валидация не прошла - показываем форму с ошибками
            return View(pet);
        }

        // GET: /Pets/Delete/{id}
        public IActionResult Delete(int id)
        {
            var pet = _petService.GetPetById(id);
            if (pet == null)
                return NotFound();
            return View(pet);
        }

        // POST: /Pets/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _petService.DeletePet(id);
            TempData["Message"] = "Питомец удалён!";
            return RedirectToAction("Index");
        }
    }
}