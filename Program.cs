// Корректные using директивы
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using practika.Services; // Ваш сервис (убедитесь, что правильное имя проекта)

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

// Регистрация нашего сервиса
builder.Services.AddScoped<IQuestionService, QuestionService>();

// Настройка CORS (не COBS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Конфигурация pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();  
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();