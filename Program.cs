using Microsoft.EntityFrameworkCore;
using WebAPI.DB;
using WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlServerRepository<>));
}
else
{
    builder.Services.AddScoped(typeof(IRepository<>), typeof(NpgsqlRepository<>));
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<SqlHotelDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));
}
else
{
    builder.Services.AddDbContext<NpgsqlHotelDbContext>(options =>
        options.UseNpgsql(ConnectionHelper.GetConnectionString(builder.Configuration)));

}


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    var scope = app.Services.CreateScope();
    await DataHelper.ManageDataAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins"); // Apply the CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();
