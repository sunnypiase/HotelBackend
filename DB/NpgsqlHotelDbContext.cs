using Microsoft.EntityFrameworkCore;
using WebAPI.Entity;

public class NpgsqlHotelDbContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public NpgsqlHotelDbContext() : base()
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.Entity<Room>().HasData(
                new() { Id = 1 },
                new() { Id = 2 },
                new() { Id = 3 },
                new() { Id = 4 },
                new() { Id = 5 },
                new() { Id = 6 }
            );
}