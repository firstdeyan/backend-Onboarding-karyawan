// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using lmsAPI.Data;

#nullable disable

namespace lmsAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("lmsAPI.activities", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("activity_description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("activity_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("category_id")
                        .HasColumnType("integer");

                    b.Property<string>("cover")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("id");

                    b.HasIndex("category_id");

                    b.ToTable("activities");
                });

            modelBuilder.Entity("lmsAPI.activities_owned", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("activities_id")
                        .HasColumnType("integer");

                    b.Property<string>("activity_note")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("category_id")
                        .HasColumnType("integer");

                    b.Property<string>("end_date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mentor_email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.Property<string>("start_date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("user_email")
                        .IsRequired()
                        .HasColumnType("character varying(45)");

                    b.HasKey("id");

                    b.HasIndex("activities_id");

                    b.HasIndex("category_id");

                    b.HasIndex("user_email");

                    b.ToTable("activities_owned");
                });

            modelBuilder.Entity("lmsAPI.activity_details", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("activity_id")
                        .HasColumnType("integer");

                    b.Property<string>("detail_desc")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("detail_link")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("detail_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("detail_type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("detail_urutan")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("activity_id");

                    b.ToTable("activity_details");
                });

            modelBuilder.Entity("lmsAPI.admin", b =>
                {
                    b.Property<string>("email")
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.Property<bool>("active")
                        .HasColumnType("boolean");

                    b.Property<string>("admin_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("birthdate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<int>("jobtitle_id")
                        .HasColumnType("integer");

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("photo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("role_id")
                        .HasColumnType("integer");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.HasKey("email");

                    b.HasIndex("jobtitle_id");

                    b.HasIndex("role_id");

                    b.ToTable("admin");
                });

            modelBuilder.Entity("lmsAPI.categories", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("category_description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("category_name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("lmsAPI.job_titles", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("jobtitle_description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("jobtitle_name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("id");

                    b.ToTable("job_titles");
                });

            modelBuilder.Entity("lmsAPI.roles", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("role_description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("role_name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("role_platform")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("lmsAPI.user", b =>
                {
                    b.Property<string>("email")
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.Property<bool>("active")
                        .HasColumnType("boolean");

                    b.Property<int>("assignedActivities")
                        .HasColumnType("integer");

                    b.Property<string>("birthdate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("finishedActivities")
                        .HasColumnType("integer");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<int>("jobtitle_id")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("photo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<double>("progress")
                        .HasColumnType("double precision");

                    b.Property<int>("role_id")
                        .HasColumnType("integer");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.HasKey("email");

                    b.HasIndex("jobtitle_id");

                    b.HasIndex("role_id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("lmsAPI.activities", b =>
                {
                    b.HasOne("lmsAPI.categories", "category_")
                        .WithMany()
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category_");
                });

            modelBuilder.Entity("lmsAPI.activities_owned", b =>
                {
                    b.HasOne("lmsAPI.activities", "activities_")
                        .WithMany()
                        .HasForeignKey("activities_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lmsAPI.categories", "category_")
                        .WithMany()
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lmsAPI.user", "user_")
                        .WithMany()
                        .HasForeignKey("user_email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("activities_");

                    b.Navigation("category_");

                    b.Navigation("user_");
                });

            modelBuilder.Entity("lmsAPI.activity_details", b =>
                {
                    b.HasOne("lmsAPI.activities", "activity_")
                        .WithMany()
                        .HasForeignKey("activity_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("activity_");
                });

            modelBuilder.Entity("lmsAPI.admin", b =>
                {
                    b.HasOne("lmsAPI.job_titles", "jobtitle_")
                        .WithMany()
                        .HasForeignKey("jobtitle_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lmsAPI.roles", "role_")
                        .WithMany()
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("jobtitle_");

                    b.Navigation("role_");
                });

            modelBuilder.Entity("lmsAPI.user", b =>
                {
                    b.HasOne("lmsAPI.job_titles", "jobtitle_")
                        .WithMany()
                        .HasForeignKey("jobtitle_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lmsAPI.roles", "role_")
                        .WithMany()
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("jobtitle_");

                    b.Navigation("role_");
                });
#pragma warning restore 612, 618
        }
    }
}
