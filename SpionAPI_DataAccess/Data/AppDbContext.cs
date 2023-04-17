using Microsoft.EntityFrameworkCore;
using SpionAPI.Models;
using SpionAPI_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpionAPI_DataAccess.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options ) :base(options)
        {

        }

        public DbSet<GuessData> GuessData { get; set; }

        public DbSet<GameStatistics> GameStatistics { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
