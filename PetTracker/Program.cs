using Microsoft.EntityFrameworkCore;
using PetTracker.Data;
using PetTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку MVC
builder.Services.AddControllersWithViews();

// Регистрация сервиса
builder.Services.AddScoped<IPetService, PetService>();

// Подключение к базе данных SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=pets.db"));

var app = builder.Build();

// Настройка конвейера HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//URL разбирается: controller = "Pets", action = "Index"
//Запрос направляется в контроллер PetsController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pets}/{action=Index}/{id?}");

app.Run();