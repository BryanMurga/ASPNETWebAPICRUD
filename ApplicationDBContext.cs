using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApi9A.models;

namespace TestApi9A
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
            //_configuration = configuration;
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("MySqlDatabase");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }*/

        public DbSet<Comment>? Comments { get; set; } //Mapear modelo con la tabla de la base de datos

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment()
                {
                    Id = 1,
                    Title = "Test",
                    Description="Tkhaskhkhsk",
                    Author = "Javier Garduño",
                    CreatedAt = new DateTime()
                },
                new Comment()
                {
                    Id = 2,
                    Title = "Test",
                    Description="Testsss",
                    Author = "María Rojo",
                    CreatedAt = new DateTime()
                }
            );
        }

    }
}