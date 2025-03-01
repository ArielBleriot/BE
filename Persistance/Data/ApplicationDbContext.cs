namespace BridgeRTU.Persistance.Data
{
    using BridgeRTU.Domain;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // DbSets for your entities (tables in the database)
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<PasswordResetToken> PasswordResetToken { get; set; }
        public DbSet<ActivityRegistration> ActivityRegistration { get; set; }
        // Optional: Configure relationships or additional configurations (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
            .Property(u => u.PersonalInterests)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),  // Convert list to JSON string when saving
                v => JsonConvert.DeserializeObject<List<string>>(v)  // Convert back when reading from DB
            );
            modelBuilder.Entity<Comment>()
     .HasOne(c => c.Project)  // Assuming a navigation property for Project in Comment
     .WithMany(p => p.Comments)  // Assuming a navigation property for Comments in Project
     .HasForeignKey(c => c.ProjectId)  // Foreign key property
     .OnDelete(DeleteBehavior.Restrict); // No cascade delete
            modelBuilder.Entity<Activity>()
           .Property(u => u.Interest)
           .HasConversion(
               v => JsonConvert.SerializeObject(v),  // Convert list to JSON string when saving
               v => JsonConvert.DeserializeObject<List<string>>(v)  // Convert back when reading from DB
           );
            modelBuilder.Entity<Activity>().HasData(
           new Activity
           {
               Id = 1,
               Name = "Yoga in the Park",
               Description = "A relaxing yoga session for all skill levels, perfect for beginners.",
               ImageUrl = "https://images.unsplash.com/photo-1540575467063-178a50c2df87?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
               Date = new DateTime(2024, 12, 10, 7, 0, 0),  // December 10, 2024, at 7:00 AM
               Status ="Sold Out",
               NoOfSeats =15,
               SeatsLeft=0,
               Location = "Central Park, New York",
               Interest = new List<string> { "Wellness" }
           },
           new Activity
           {
               Id = 2,
               Name = "Coding Bootcamp",
               Description = "An intensive coding bootcamp for beginners looking to learn web development.",
               ImageUrl = "https://images.unsplash.com/photo-1505373877841-8d25f7d46678?q=80&w=2012&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
               Date = new DateTime(2024, 12, 15, 9, 0, 0),  // December 15, 2024, at 9:00 AM
               Location = "Tech Hub, San Francisco",
               Interest = new List<string> { "Technology" },
               Status = "Available",
               NoOfSeats = 15,
               SeatsLeft = 5,
           },
           new Activity
           {
               Id = 3,
               Name = "Photography Workshop",
               Description = "A photography workshop to help you master the basics of taking professional photos.",
               ImageUrl = "https://images.unsplash.com/photo-1576085898323-218337e3e43c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
               Date = new DateTime(2024, 12, 20, 10, 0, 0),  // December 20, 2024, at 10:00 AM
               Location = "Golden Gate Bridge, San Francisco",
               Interest = new List<string> { "Photography" },
               Status = "Available",
               NoOfSeats = 15,
               SeatsLeft = 10,
           },
           new Activity
           {
               Id = 4,
               Name = "Cooking Class",
               Description = "Learn to cook delicious Italian dishes with an expert chef.",
               ImageUrl = "https://images.unsplash.com/photo-1560523160-754a9e25c68f?q=80&w=2036&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
               Date = new DateTime(2024, 12, 22, 14, 0, 0),  // December 22, 2024, at 2:00 PM
               Location = "Italian Kitchen, Los Angeles",
               Interest = new List<string> { "Cooking" },
               Status = "Sold Out",
               NoOfSeats = 15,
               SeatsLeft = 0,
           }
           );

        }
    }

}
