﻿// <auto-generated />
using System;
using BridgeRTU.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BridgeRTU.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250202000759_ActivityUpdate")]
    partial class ActivityUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BridgeRTU.Domain.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interest")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoOfSeats")
                        .HasColumnType("int");

                    b.Property<int>("SeatsLeft")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Activity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2024, 12, 10, 7, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A relaxing yoga session for all skill levels, perfect for beginners.",
                            ImageUrl = "https://images.unsplash.com/photo-1540575467063-178a50c2df87?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            Interest = "[\"Wellness\"]",
                            Location = "Central Park, New York",
                            Name = "Yoga in the Park",
                            NoOfSeats = 15,
                            SeatsLeft = 0,
                            Status = "Sold Out"
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2024, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "An intensive coding bootcamp for beginners looking to learn web development.",
                            ImageUrl = "https://images.unsplash.com/photo-1505373877841-8d25f7d46678?q=80&w=2012&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            Interest = "[\"Technology\"]",
                            Location = "Tech Hub, San Francisco",
                            Name = "Coding Bootcamp",
                            NoOfSeats = 15,
                            SeatsLeft = 5,
                            Status = "Available"
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2024, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A photography workshop to help you master the basics of taking professional photos.",
                            ImageUrl = "https://images.unsplash.com/photo-1576085898323-218337e3e43c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            Interest = "[\"Photography\"]",
                            Location = "Golden Gate Bridge, San Francisco",
                            Name = "Photography Workshop",
                            NoOfSeats = 15,
                            SeatsLeft = 10,
                            Status = "Available"
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2024, 12, 22, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Learn to cook delicious Italian dishes with an expert chef.",
                            ImageUrl = "https://images.unsplash.com/photo-1560523160-754a9e25c68f?q=80&w=2036&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            Interest = "[\"Cooking\"]",
                            Location = "Italian Kitchen, Los Angeles",
                            Name = "Cooking Class",
                            NoOfSeats = 15,
                            SeatsLeft = 0,
                            Status = "Sold Out"
                        });
                });

            modelBuilder.Entity("BridgeRTU.Domain.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("BridgeRTU.Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("Interests")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("Skills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("BridgeRTU.Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldOfStudy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalInterests")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RememberMe")
                        .HasColumnType("bit");

                    b.Property<string>("UniversityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("BridgeRTU.Domain.Project", b =>
                {
                    b.HasOne("BridgeRTU.Domain.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
