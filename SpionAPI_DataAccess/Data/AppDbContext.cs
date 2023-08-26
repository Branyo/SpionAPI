using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpionAPI.Models;
using SpionAPI_DataAccess.Data.Configurations;
using SpionAPI_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpionAPI_DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<SpionUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options ) :base(options)
        {

        }

        public DbSet<GuessData> GuessData { get; set; }

        public DbSet<GameStatistics> GameStatistics { get; set; }

        public DbSet<SpionUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

        }


    }
}
