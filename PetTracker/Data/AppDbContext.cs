using Microsoft.EntityFrameworkCore;
using PetTracker.Models;

namespace PetTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //EF Core преобразует LINQ-запрос в SQL
        //Выполняет запрос к файлу pets.db
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Начальные данные
            modelBuilder.Entity<Pet>().HasData(
                new Pet { Id = 1, Name = "Барсик", Type = "Кот", Birthday = new DateTime(2020, 5, 15), Notes = "Любит спать на диване" },
                new Pet { Id = 2, Name = "Шарик", Type = "Собака", Birthday = new DateTime(2019, 8, 20), Notes = "Гуляет по утрам" }
            );
        }
    }
}