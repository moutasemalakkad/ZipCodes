using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Zipcodes_ASPNET.Models
{
    public partial class zipCodesDBContext : DbContext
    {
        public zipCodesDBContext()
        {
        }

        public zipCodesDBContext(DbContextOptions<zipCodesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ZipCodesWithDistance> ZipCodesWithDistances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Please change the connection string according to your server and database 
                optionsBuilder.UseMySQL("Server=moutasemserver.mysql.database.azure.com; Port=3306; Database=zipcodesdb; Uid=moutasemakkad@moutasemserver; " +
                    "Pwd=Geozip123.; Persist Security Info = False; Connect Timeout = 3000");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ZipCodesWithDistance>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.miToZcta5).HasColumnName("mi_to_zcta5");
                entity.Property(e => e.Zip1).HasMaxLength(20).IsUnicode(false).HasColumnName("zip1");
                entity.Property(e => e.Zip2).HasMaxLength(20).IsUnicode(false).HasColumnName("zip2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
