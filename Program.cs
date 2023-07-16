using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using WebAPI.DB;
using WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddDbContext<HotelDbContext>(optionsBuilder => UseServer(optionsBuilder));


var app = builder.Build();
var scope = app.Services.CreateScope();
await DataHelper.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void UseServer(DbContextOptionsBuilder optionsBuilder)
{
    if (builder.Environment.IsDevelopment())
    {
        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
    }
    else
    {
        optionsBuilder.UseSqlServer(ConnectionHelper.GetConnectionString(builder.Configuration));
    }
}