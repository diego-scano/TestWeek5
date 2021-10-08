using GestioneSpeseEF.EF.Configurations;
using GestioneSpeseEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GestioneSpeseEF.EF
{
    public class SpesaContext : DbContext
    {
        public DbSet<Spesa> Spese { get; set; }
        public DbSet<Categoria> Categorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                string connectionStringSQL = config.GetConnectionString("AcademyG");

                optionsBuilder.UseSqlServer(connectionStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SpesaConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        }
    }
}
