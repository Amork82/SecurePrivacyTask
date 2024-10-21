using SecurePrivacyTask.Server.Commons;
using SecurePrivacyTask.Server.DBContext;
using SecurePrivacyTask.Server.Services;
using SecurePrivacyTask.Server.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Activated methods and actions
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod() // Consente i metodi GET, POST, DELETE, ecc.
                .AllowAnyHeader();
        });
});

// Register MongoDB Settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Add a singleton service for MongoClient
builder.Services.AddSingleton<MongoDBContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BaseService>();

// Add register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll"); // Applied policy CORS

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
