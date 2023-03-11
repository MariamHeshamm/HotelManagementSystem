using EF_Project.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.context
{
    public class LoginManagerDB:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Data Source=DESKTOP-0H4VTQ3\\SQLEXPRESS;Initial Catalog=LoginManagerDB;Integrated Security=True;Encrypt=false");
        public virtual DbSet<Frontend>Frontends { get; set; }
        public virtual DbSet<KitchenManager>KitchenManagers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Frontend>(EntityBuilder =>
            {
                EntityBuilder.HasKey(e => e.user_name);
                EntityBuilder.Property(e => e.user_name).HasMaxLength(50);
                EntityBuilder.Property(e => e.pass_word)
                .IsRequired().HasMaxLength(50);            
            });

            modelBuilder.Entity<Entities.KitchenManager>(EntityBuilder =>
            {
                EntityBuilder.HasKey(e => e.user_name);
                EntityBuilder.Property(e => e.user_name).HasMaxLength(50);
                EntityBuilder.Property(e => e.pass_word)
                .IsRequired().HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
