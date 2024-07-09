using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrinceQ.Models.Entities;

namespace PrinceQ.DataAccess.Data.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<ClerkDevice> Device { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Queue_Status> Queue_Statuses { get; set; }
        public virtual DbSet<Queues> QueueNumbers { get; set; }
        public virtual DbSet<Clerk_Serve_ForFilling> Clerk_Serve_ForFilling { get; set; }
        public virtual DbSet<Clerk_Serve_Releasing> Clerk_Serve_Releasing { get; set; }
        public virtual DbSet<User_Category> User_Category { get; set; }
        public virtual DbSet<Serving> Serving { get; set; }
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<User_Category> User_Categories{ get; set; }
        public virtual DbSet<IsActive> IsActive{ get; set; }
        public virtual DbSet<Stage_Queue> Stage{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding

            //START OF TEMPORARY
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = "3386761a-6384-4e97-9eb3-d2d09e6bfec5",
                        UserName = "user1",
                        NormalizedUserName = "USER1",
                        Email = "user1@princeretail.com",
                        NormalizedEmail = "USER1@PRINCERETAIL.COM",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        PasswordHash = "AQAAAAIAAYagAAAAECnnqwWvQxblMxWGHvFRkui6EcfZu6BPqf2MtI8fZS9u6gCf8BWu3bZIc1xF16W6zA==",
                        SecurityStamp = "GJ636UXBBLKO5JOGP6X3WISMIEBRVVHM",
                        ConcurrencyStamp = "79681f10-0ee2-4127-9e9c-0eb73ded94fe",
                        IsActiveId = 1
                    },
                    new User
                    {
                        Id = "5817a627-dcb6-4612-85e5-13b56dc52560",
                        UserName = "user2",
                        NormalizedUserName = "USER2",
                        Email = "user2@princeretail.com",
                        NormalizedEmail = "USER2@PRINCERETAIL.COM",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        PasswordHash = "AQAAAAIAAYagAAAAEHVWq2Lhee6lgjGXwYSWbtfzS4JYT0MEYWaOs6USpgbOBGJEaO1KB8YxDLviUH8eag==",
                        SecurityStamp = "TH3JDB7PM7WMDGG4OYPLQKLGA5ECC4T3",
                        ConcurrencyStamp = "c935029b-a917-4967-b007-18bd2ccaa9fb",
                        IsActiveId = 1
                    },
                    new User
                    {
                        Id = "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4",
                        UserName = "user3",
                        NormalizedUserName = "USER3",
                        Email = "user3@princeretail.com",
                        NormalizedEmail = "USER3@PRINCERETAIL.COM",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        PasswordHash = "AQAAAAIAAYagAAAAEOIf7eXfV9kI/xsnNgDanLoBQLRGl1+jrl1+BJvGqblI7v/VTSx4C3/LJTGi4oBC7Q==",
                        SecurityStamp = "5BJYPNN2TLNSI55R6RGCHCK4F3Z4QCXA",
                        ConcurrencyStamp = "ecfd0212-7008-485f-aca1-ba1c17ac009c",
                        IsActiveId = 1
                    },

                    new User
                    {
                        Id = "cadf9177-f64e-4ab6-bb37-f50770ef67b5",
                        UserName = "personnel1",
                        NormalizedUserName = "PERSONNEL1",
                        Email = "personnel1@princeretail.com",
                        NormalizedEmail = "PERSONNEL1@PRINCERETAIL.COM",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        PasswordHash = "AQAAAAIAAYagAAAAEHX+4UDDHRNT58qxXn8u5YlDjkKG+bJTVvoDKEvtOPFF3/z+o/4ZbJ3CIv0+W/mZjw==",
                        SecurityStamp = "6WJLAWNDIARO6JH6N3T7AM224IYQLRO4",
                        ConcurrencyStamp = "8b25f225-70d8-48be-a06f-46a91475d5f8",
                        IsActiveId = 1
                    },
                 
                    new User
                    {
                        Id = "f626b751-35a0-43df-8173-76cb5b4886fd",
                        UserName = "Admin",
                        NormalizedUserName = "ADMIN",
                        Email = "admin@princeretail.com",
                        NormalizedEmail = "ADMIN@PRINCERETAIL.COM",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        PasswordHash = "AQAAAAIAAYagAAAAEJpB1HfJeqVp4SjIDJMOHVIEmfY55M/N7YsXHENkNlge5P6k15rYKenBWVNTobCO0Q==",
                        SecurityStamp = "N36KZ2J262FG2K5NWBB3WESIUXPLZ5WH",
                        ConcurrencyStamp = "68094bf9-de7e-4b21-a651-b741ae57c6aa",
                        IsActiveId = 1
                    }
                );

            // Seed the roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "18ab63db-22b1-4656-93e8-6240c08c988c", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "5671e11c-00b4-4302-9214-c3b7f7b71188", Name = "RegisterPersonnel", NormalizedName = "REGISTERPERSONNEL" },
                new IdentityRole { Id = "fbc43974-ddf4-4fed-8a0b-42e6897f259f", Name = "Clerk", NormalizedName = "CLERK" }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                   new { UserId = "f626b751-35a0-43df-8173-76cb5b4886fd", RoleId = "18ab63db-22b1-4656-93e8-6240c08c988c" },
                   new { UserId = "cadf9177-f64e-4ab6-bb37-f50770ef67b5", RoleId = "5671e11c-00b4-4302-9214-c3b7f7b71188" },
                   new { UserId = "3386761a-6384-4e97-9eb3-d2d09e6bfec5", RoleId = "fbc43974-ddf4-4fed-8a0b-42e6897f259f" },
                   new { UserId = "5817a627-dcb6-4612-85e5-13b56dc52560", RoleId = "fbc43974-ddf4-4fed-8a0b-42e6897f259f" },
                   new { UserId = "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4", RoleId = "fbc43974-ddf4-4fed-8a0b-42e6897f259f" }
               );

            modelBuilder.Entity<User_Category>().HasData(
                //Clerk 1
                new User_Category { UserId = "3386761a-6384-4e97-9eb3-d2d09e6bfec5", CategoryId = 1},
                new User_Category { UserId = "3386761a-6384-4e97-9eb3-d2d09e6bfec5", CategoryId = 2},
                new User_Category { UserId = "3386761a-6384-4e97-9eb3-d2d09e6bfec5", CategoryId = 3},
                new User_Category { UserId = "3386761a-6384-4e97-9eb3-d2d09e6bfec5", CategoryId = 4},
                //Clerk 2
                new User_Category { UserId = "5817a627-dcb6-4612-85e5-13b56dc52560", CategoryId = 1 },
                new User_Category { UserId = "5817a627-dcb6-4612-85e5-13b56dc52560", CategoryId = 2 },
                new User_Category { UserId = "5817a627-dcb6-4612-85e5-13b56dc52560", CategoryId = 3 },
                new User_Category { UserId = "5817a627-dcb6-4612-85e5-13b56dc52560", CategoryId = 4 },
                //Clerk 3
                new User_Category { UserId = "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4", CategoryId = 1 },
                new User_Category { UserId = "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4", CategoryId = 2 },
                new User_Category { UserId = "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4", CategoryId = 3 },
                new User_Category { UserId = "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4", CategoryId = 4 }
              );

            //END OF TEMPORARY

            //Seeding
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Category A", Description = "Trade", IsActiveId = 1 },
                new Category { CategoryId = 2, CategoryName = "Category B", Description = "Non-Trade", IsActiveId = 1 },
                new Category { CategoryId = 3, CategoryName = "Category C", Description = "Special", IsActiveId = 1 },
                new Category { CategoryId = 4, CategoryName = "Category D", Description = "Inquiry" , IsActiveId = 1 }
                );

            modelBuilder.Entity<Queue_Status>().HasData(
                 new Queue_Status { StatusId = 1, StatusName = "waiting" },
                 new Queue_Status { StatusId = 2, StatusName = "serve" },
                 new Queue_Status { StatusId = 3, StatusName = "reserve" },
                 new Queue_Status { StatusId = 4, StatusName = "cancel" },
                 new Queue_Status { StatusId = 5, StatusName = "serving" }
                 );

            modelBuilder.Entity<IsActive>().HasData(
               new IsActive { IsActiveId = 1, Name = "Active" },
               new IsActive { IsActiveId = 2, Name = "Inactive" }
               );

            modelBuilder.Entity<Stage_Queue>().HasData(
               new Stage_Queue { StageId = 1, StageName = "Filling" },
               new Stage_Queue { StageId = 2, StageName = "Releasing" }
               );

            modelBuilder.Entity<ClerkDevice>().HasData(
               new ClerkDevice { DeviceId = "00330-71344-74698-AAOEM", ClerkNumber = "Clerk 1" }
               );

            // For Junction 
            modelBuilder.Entity<User>()
              .Property(u => u.Created_At)
              .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Queues>()
                .HasKey(uc => new { uc.QueueId, uc.CategoryId, uc.QueueNumber});
                
            modelBuilder.Entity<User_Category>()
                .HasKey(uc => new { uc.UserId, uc.CategoryId });

            modelBuilder.Entity<User_Category>()
                .HasKey(uc => new { uc.UserId, uc.CategoryId });

            modelBuilder.Entity<Clerk_Serve_ForFilling>()
                .HasKey(uc => new { uc.GenerateDate, uc.ClerkId, uc.CategoryId, uc.QueueNumber });

            modelBuilder.Entity<Clerk_Serve_Releasing>()
                .HasKey(uc => new { uc.GenerateDate, uc.ClerkId, uc.CategoryId, uc.QueueNumber });

            // Default Date
            modelBuilder.Entity<Category>()
                .Property(u => u.Created_At)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Queue_Status>()
                .Property(u => u.Created_At)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Serving>()
                .Property(u => u.Served_At)
                .HasDefaultValueSql("GETDATE()");
            
            modelBuilder.Entity<Announcement>()
                .Property(u => u.Created_At)
                .HasDefaultValueSql("GETDATE()");

        }

    }

}

//roleid = 18ab63db - 22b1 - 4656 - 93e8 - 6240c08c988c name - admin
//roleid = 5671e11c-00b4-4302-9214-c3b7f7b71188 name - RegisterPersonnel
//roleid = fbc43974-ddf4-4fed-8a0b-42e6897f259f name - Clerk

//userid = f626b751 - 35a0 - 43df - 8173 - 76cb5b4886fd  roleid = 18ab63db - 22b1 - 4656 - 93e8 - 6240c08c988c
//userid = cadf9177-f64e-4ab6-bb37-f50770ef67b5  roleid = 5671e11c-00b4-4302-9214-c3b7f7b71188
//userid = 3386761a-6384-4e97-9eb3-d2d09e6bfec5  roleid = fbc43974-ddf4-4fed-8a0b-42e6897f259f
//userid = 5817a627-dcb6-4612-85e5-13b56dc52560  roleid = fbc43974-ddf4-4fed-8a0b-42e6897f259f
//userid = 6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4  roleid = fbc43974-ddf4-4fed-8a0b-42e6897f259f

