using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

#nullable disable
namespace MyWebApp.Models
{

    public static class ContextExtensions
    {
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var name = entityType!.GetTableName();

            return name;
        }
    }

    public partial class DBContext : DbContext
    {
        private readonly string _connectionString;

        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<StateModel> States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedOnAdd().HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("user_fullname");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.HasIndex(u => u.UserName).IsUnique();
            });

            modelBuilder.Entity<StateModel>(entity =>
            {
                entity.ToTable("states");

                entity.HasKey(e => e.StateId);

                entity.Property(e => e.StateId).ValueGeneratedOnAdd().HasColumnName("state_id");

                entity.Property(e => e.Page)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasColumnName("state_page");

                entity.Property(e => e.IsDesc)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasColumnName("state_isdesc");

                entity.Property(e => e.SortBy)
                    .IsRequired()
                    .HasColumnName("state_sortby");

                entity.Property(e => e.FilterParam)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("state_filter");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_state_id")
                   .IsRequired();
            });

            modelBuilder.Entity<UserModel>()
                .HasOne(b => b.State)
                .WithOne(i => i.User)
                .HasForeignKey<StateModel>(b => b.UserId);

            OnModelCreatingPartial(modelBuilder);
        }
    }
}
