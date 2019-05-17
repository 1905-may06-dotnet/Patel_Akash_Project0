using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pizzaboxdata.Data
{
    public partial class PizzaContext : DbContext
    {
        public PizzaContext()
        {
        }

        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LocationTable> LocationTable { get; set; }
        public virtual DbSet<OrderTable> OrderTable { get; set; }
        public virtual DbSet<PizzaTable> PizzaTable { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=akashmukeshpatel.database.windows.net; Database=Pizzadb;user id=akashmukeshpatel; Password=Troll1!type;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<LocationTable>(entity =>
            {
                entity.HasKey(e => e.LocationPk)
                    .HasName("PK__Location__D2BADFE061EAB28D");

                entity.ToTable("Location_Table");

                entity.Property(e => e.LocationPk)
                    .HasColumnName("Location_PK")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<OrderTable>(entity =>
            {
                entity.HasKey(e => e.OrderIdPk)
                    .HasName("PK__Order_Ta__74B0CAFEFF4AE726");

                entity.ToTable("Order_Table");

                entity.Property(e => e.OrderIdPk)
                    .HasColumnName("OrderID_PK")
                    .ValueGeneratedNever();

                entity.Property(e => e.LocationFk)
                    .HasColumnName("Location_FK")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDateTime).HasColumnType("datetime");

                entity.Property(e => e.UsernameFk)
                    .HasColumnName("Username_FK")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.LocationFkNavigation)
                    .WithMany(p => p.OrderTable)
                    .HasForeignKey(d => d.LocationFk)
                    .HasConstraintName("FK__Order_Tab__Locat__5165187F");

                entity.HasOne(d => d.UsernameFkNavigation)
                    .WithMany(p => p.OrderTable)
                    .HasForeignKey(d => d.UsernameFk)
                    .HasConstraintName("FK__Order_Tab__Usern__5070F446");
            });

            modelBuilder.Entity<PizzaTable>(entity =>
            {
                entity.HasKey(e => e.PizzaIdPk)
                    .HasName("PK__Pizza_Ta__9FBAC6D619F19711");

                entity.ToTable("Pizza_Table");

                entity.Property(e => e.PizzaIdPk)
                    .HasColumnName("PizzaID_PK")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderIdFk).HasColumnName("OrderID_FK");

                entity.Property(e => e.PizzaString)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrderIdFkNavigation)
                    .WithMany(p => p.PizzaTable)
                    .HasForeignKey(d => d.OrderIdFk)
                    .HasConstraintName("FK__Pizza_Tab__Order__59FA5E80");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UsernamePk)
                    .HasName("PK__User_Tab__B0AC56E9910EC75F");

                entity.ToTable("User_Table");

                entity.Property(e => e.UsernamePk)
                    .HasColumnName("Username_PK")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}
