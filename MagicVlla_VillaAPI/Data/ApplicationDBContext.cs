using MagicVlla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVlla_VillaAPI.Data
{
    public class ApplicationDBContext : DbContext

    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<VillaNumber> VillaNumber { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationship between Villa and VillaNumber
            modelBuilder.Entity<VillaNumber>()
                .HasOne(vn => vn.Villa)
                .WithMany() // Assuming each Villa can have multiple VillaNumbers, adjust if needed
                .HasForeignKey(vn => vn.villaID);
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Pool View",
                    Description = "A beautiful villa with a view of the pool.",
                    Rate = 200.0,
                    sqft = 1500,
                    Occupancy = 4,

                    ImageUrl = "https://images.unsplash.com/photo-1610641818989-c2051b5e2cfd?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Amenity = "Pool, WiFi, Parking",
                    CreatedDate = DateTime.Now

                },
                new Villa
                {
                    Id = 2,
                    Name = "Ocean Breeze",
                    Description = "A relaxing villa with an ocean breeze.",
                    Rate = 300.0,
                    sqft = 2000,
                    Occupancy = 6,
                    ImageUrl = "https://images.unsplash.com/photo-1582719508461-905c673771fd?q=80&w=1450&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Amenity = "Ocean View, WiFi, Parking",
                    CreatedDate = DateTime.Now

                },
                new Villa
                {
                    Id = 3,
                    Name = "Mountain Retreat",
                    Description = "A peaceful retreat in the mountains.",
                    Rate = 250.0,
                    sqft = 2500,
                    Occupancy = 8,
                    ImageUrl = "https://images.unsplash.com/photo-1540541338287-41700207dee6?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Amenity = "Mountain View, Fireplace, WiFi",
                    CreatedDate = DateTime.Now

                },
                new Villa
                {
                    Id = 4,
                    Name = "Sunset Haven",
                    Description = "A haven with beautiful sunset views.",
                    Rate = 220.0,
                    sqft = 1800,
                    Occupancy = 5,
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1681922761648-d5e2c3972982?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8cmVzb3J0fGVufDB8fDB8fHww",
                    Amenity = "Sunset View, WiFi, Parking",
                    CreatedDate = DateTime.Now

                },
                new Villa
                {
                    Id = 5,
                    Name = "Garden Oasis",
                    Description = "An oasis surrounded by gardens.",
                    Rate = 180.0,
                    sqft = 1200,
                    Occupancy = 3,
                    ImageUrl = "https://images.unsplash.com/photo-1563911302283-d2bc129e7570?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Amenity = "Garden, WiFi, Parking",
                    CreatedDate = DateTime.Now

                },
                new Villa
                {
                    Id = 6,
                    Name = "Beachfront Bliss",
                    Description = "A blissful villa right on the beach.",
                    Rate = 350.0,
                    sqft = 2200,
                    Occupancy = 7,
                    ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Amenity = "Beachfront, WiFi, Parking",
                    CreatedDate = DateTime.Now



                }


                );
        }
    }
}
