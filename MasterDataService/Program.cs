using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Регистрация контроллеров
builder.Services.AddControllers();

// 2. (Опционально) Swagger для тестирования
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Репозитории
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
Console.WriteLine("asdasdasd");
// Сервисы
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
//builder.Services.AddScoped<ITagService, TagService>();
//builder.Services.AddScoped<IFormulaService, FormulaService>();
//builder.Services.AddScoped<IMeasurementUnitService, MeasurementUnitService>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());


 

var app = builder.Build();

// 3. (Опционально) Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 4. Маппинг контроллеров (роутинг)
app.MapControllers();

app.Run();