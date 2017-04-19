using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TheBible.Model.DataLoader
{
    /// <summary>
    /// SQLitePrefs holds all of the preferences data for users. It uses
    /// Entity Framework Core for data persistence.
    /// </summary>
    public class SQLitePrefs : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string filePath = "biblesqlprefs.db";
            string dsn = String.Format(@"Data Source={0}", filePath);
            
            optionsBuilder.UseSqlite(dsn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
