﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SearchEngine.DataAccess;

namespace SearchEngine.Web.Migrations
{
    [DbContext(typeof(SearchEngineContext))]
    [Migration("20200118100901_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SearchEngine.Models.Database.Business.Search", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KeyWords")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SearchEngineTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SearchEngineTypeId");

                    b.ToTable("Search","Business");
                });

            modelBuilder.Entity("SearchEngine.Models.Database.Business.SearchResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SearchId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SearchId");

                    b.ToTable("SearchResult","Business");
                });

            modelBuilder.Entity("SearchEngine.Models.Database.Dictionaries.SearchEngineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SearchEngineType","Dictionaries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "Google",
                            Description = "Поисковая система Google"
                        },
                        new
                        {
                            Id = 2,
                            Code = "Yandex",
                            Description = "Поисковая система Яндекс"
                        },
                        new
                        {
                            Id = 3,
                            Code = "Bing",
                            Description = "Поисковая система Bing"
                        });
                });

            modelBuilder.Entity("SearchEngine.Models.Database.Business.Search", b =>
                {
                    b.HasOne("SearchEngine.Models.Database.Dictionaries.SearchEngineType", "SearchEngineType")
                        .WithMany()
                        .HasForeignKey("SearchEngineTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SearchEngine.Models.Database.Business.SearchResult", b =>
                {
                    b.HasOne("SearchEngine.Models.Database.Business.Search", "Search")
                        .WithMany("SearchResults")
                        .HasForeignKey("SearchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
