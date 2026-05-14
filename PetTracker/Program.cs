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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PetTracker.Data.AppDbContext>();
        // Эта команда создаст файл базы и все таблицы, если их нет
        context.Database.EnsureCreated(); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при создании базы данных.");
    }
}

app.Run();
