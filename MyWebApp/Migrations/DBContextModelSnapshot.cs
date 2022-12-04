﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebApp.Models;

#nullable disable

namespace MyWebApp.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyWebApp.Models.SleepEntryModel", b =>
                {
                    b.Property<int>("SleepEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("sleep_entry_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SleepEntryId"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("sleep_entry_date");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("user_isdeleted");

                    b.Property<int>("SleepDuration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("sleep_entry_page");

                    b.Property<int>("SleepTime")
                        .HasColumnType("int")
                        .HasColumnName("sleep_entry_wakeuptime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SleepEntryId");

                    b.HasIndex("UserId");

                    b.ToTable("sleep_entries", (string)null);
                });

            modelBuilder.Entity("MyWebApp.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_email");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("user_fullname");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("user_isadmin");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("user_isdeleted");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_password");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_name");

                    b.HasKey("UserId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MyWebApp.Models.SleepEntryModel", b =>
                {
                    b.HasOne("MyWebApp.Models.UserModel", "User")
                        .WithMany("SleepEntries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyWebApp.Models.UserModel", b =>
                {
                    b.Navigation("SleepEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
