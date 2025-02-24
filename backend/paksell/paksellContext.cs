using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell.Db 
{
    public class paksellContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<CityArea> CityAreas { get; set; }
        public DbSet<AdvertisementCategory> advertisementCategories { get; set; }
        public DbSet<AdvertisementFeature> advertisementFeatures { get; set; }
        public DbSet<AdvertisementImage> AdvertisementImages { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PakSell;user id=sa;password=123;Encrypt=True;TrustServerCertificate=True");


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advertisement>().ToTable("Advertisements");
            modelBuilder.Entity<Advertisement>().HasKey(p => p.Id);

            //one-to-many  advetisement and images
            modelBuilder.Entity<Advertisement>().HasMany<AdvertisementImage>(a => a.AdvertisementImages)
                .WithOne().OnDelete(DeleteBehavior.Cascade);
            //one-to-many  advetisement and features
            modelBuilder.Entity<Advertisement>().HasMany<AdvertisementFeature>(a => a.AdvertisementFeatures)
                .WithOne().OnDelete(DeleteBehavior.Cascade);

           // modelBuilder.Entity<Advertisement>().HasOne<User>(a=>a.PostedBy).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Advertisement>()
           .HasOne<User>(a => a.PostedBy)
           .WithMany()
           .HasForeignKey(a => a.PostedById)  // Keep it as int
           .OnDelete(DeleteBehavior.Restrict);


        }


    }
}

