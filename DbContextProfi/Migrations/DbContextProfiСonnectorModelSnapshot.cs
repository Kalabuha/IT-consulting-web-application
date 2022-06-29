﻿// <auto-generated />
using System;
using DbContextProfi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbContextProfi.Migrations
{
    [DbContext(typeof(DbContextProfiСonnector))]
    partial class DbContextProfiСonnectorModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Resources.Entities.ApplicationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateReceiptApplication")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date_receipt_application");

                    b.Property<string>("GuestEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Guest_email");

                    b.Property<string>("GuestName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("Guest_name");

                    b.Property<string>("GuestsApplicationText")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("Guests_application_text");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("Application_processing_status");

                    b.HasKey("Id");

                    b.ToTable("Guests_applications");
                });

            modelBuilder.Entity("Resources.Entities.BlogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("BlogImageAsArray64")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Blog_image");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit")
                        .HasColumnName("Is_published");

                    b.Property<string>("LongBlogDescription")
                        .IsRequired()
                        .HasMaxLength(6000)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Long_description");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Creation_date");

                    b.Property<string>("ShortBlogDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Short_description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Resources.Entities.ContactEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Address");

                    b.Property<string>("Fax")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Fax");

                    b.Property<bool>("IsPostedOnThePage")
                        .HasColumnType("bit")
                        .HasColumnName("Is_posted_on_the_page");

                    b.Property<byte[]>("MapAsArray64")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Map");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Phone");

                    b.Property<int>("Postcode")
                        .HasColumnType("int")
                        .HasColumnName("Postcode");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Resources.Entities.HomePageImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("ImageAsArray64")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Image");

                    b.Property<bool>("IsPostedOnThePage")
                        .HasColumnType("bit")
                        .HasColumnName("Is_posted_on_the_page");

                    b.HasKey("Id");

                    b.ToTable("Home_page_images");
                });

            modelBuilder.Entity("Resources.Entities.HomePageTextEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsPostedOnThePage")
                        .HasColumnType("bit")
                        .HasColumnName("Is_posted_on_the_page");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("Text");

                    b.HasKey("Id");

                    b.ToTable("Home_page_text");
                });

            modelBuilder.Entity("Resources.Entities.ProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("CustomerCompanyLogoAsArray64")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Customer_company_logo");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit")
                        .HasColumnName("Is_published");

                    b.Property<string>("LinkToCustomerSite")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Link_to_customer_site");

                    b.Property<string>("ProjectDescription")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Project_article");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Resources.Entities.ServiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit")
                        .HasColumnName("Is_published");

                    b.Property<string>("ServiceDescription")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)")
                        .HasColumnName("Service_description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
