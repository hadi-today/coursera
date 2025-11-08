// Program.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. Add Authentication Services
// نکته: در یک پروژه واقعی، Authority و Audience باید از فایل تنظیمات خوانده شوند
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // This is for demonstration. In a real app, use a real identity provider.
        options.Authority = "https://your-auth-server.com/"; // آدرس سرور احراز هویت شما
        options.Audience = "your-api-resource"; // نام API شما
        options.RequireHttpsMetadata = false; // فقط برای تست در حالت توسعه
    });
builder.Services.AddAuthorization();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// 1. Error Handling Middleware (First)
// این باید اولین Middleware باشد تا خطاهای بعدی را بگیرد
app.UseMiddleware<ErrorHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. Authentication and Authorization Middleware (Next)
// این دو باید قبل از MapControllers باشند
app.UseAuthentication();
app.UseAuthorization();

// 3. Custom Logging Middleware (Last)
app.UseMiddleware<LoggingMiddleware>();


app.MapControllers();

app.Run();