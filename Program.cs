using Microsoft.EntityFrameworkCore;
using WebAPI.DB;
using WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins(
                   "http://localhost:3000",
                   "https://64b776f28321af4be0486692--pegas.netlify.app/",
                   "https://64b7bd78d57021782c12fae6--pegas.netlify.app")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
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

app.UseCors("AllowSpecificOrigins"); // Apply the CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();
