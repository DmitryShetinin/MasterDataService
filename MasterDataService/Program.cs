using Application.Interfaces;
using Confluent.Kafka;
using Infrastructure.Messaging;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Регистрация контроллеров
builder.Services.AddControllers();


// 2. (Опционально) Swagger для тестирования
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(EquipmentService).Assembly));

var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<ProducerConfig>()
    ?? new ProducerConfig
    {
      BootstrapServers = "localhost:9092", // или из переменных окружения
      ClientId = "master-data-service",

    };

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(kafkaConfig);
// Репозитории
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Сервисы
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IMessageProducer, KafkaMessageProducer>();



//builder.Services.AddScoped<ITagService, TagService>();
//builder.Services.AddScoped<IFormulaService, FormulaService>();
//builder.Services.AddScoped<IMeasurementUnitService, MeasurementUnitService>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());




var app = builder.Build();

Console.WriteLine($"ENV: {app.Environment.EnvironmentName}");

Console.WriteLine(app.Environment.IsDevelopment());
// 3. (Опционально) Swagger UI
app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

// 4. Маппинг контроллеров (роутинг)
app.MapControllers();

app.Run();
