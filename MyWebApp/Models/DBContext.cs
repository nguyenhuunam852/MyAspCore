using Microsoft.EntityFrameworkCore;

#nullable disable
namespace MyWebApp.Models
{
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
        public virtual DbSet<SleepEntryModel> SleepEntries { get; set; }

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

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasColumnName("user_isdeleted");

                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasColumnName("user_isadmin");

                entity.HasIndex(u => u.UserName).IsUnique();
            });

            modelBuilder.Entity<SleepEntryModel>(entity =>
            {
                entity.ToTable("sleep_entries");

                entity.HasKey(e => e.SleepEntryId);

                entity.Property(e => e.SleepEntryId).ValueGeneratedOnAdd().HasColumnName("sleep_entry_id");

                entity.Property(e => e.SleepDuration)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasColumnName("sleep_entry_page");

                entity.Property(e => e.Date)
                   .HasColumnName("sleep_entry_date");

                entity.Property(e => e.SleepTime)
                   .HasColumnName("sleep_entry_sleeptime");

                entity.Property(e => e.SleepTime)
                   .HasColumnName("sleep_entry_wakeuptime");

                entity.Property(e => e.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(0)
                   .HasColumnName("user_isdeleted");
            });

            modelBuilder.Entity<SleepEntryModel>()
                       .HasOne(p => p.User)
                       .WithMany(b => b.SleepEntries)
                       .HasForeignKey(p => p.UserId);

            OnModelCreatingPartial(modelBuilder);
        }
    }
}
