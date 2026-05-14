using PetTracker.Models;
using PetTracker.ViewModels;
using System.Collections.Generic;

namespace PetTracker.Services
{
    public interface IPetService
    {
        // Получить всех питомцев
        List<Pet> GetAllPets();

        // Добавить питомца (из ViewModel)
        void AddPet(PetViewModel viewModel);

        // Получить питомца по ID
        Pet GetPetById(int id);

        // Получить питомцев по типу
        List<Pet> GetPetsByType(string type);

        // Обновить питомца
        void UpdatePet(Pet pet);

        // Удалить питомца
        void DeletePet(int id);
    }
}