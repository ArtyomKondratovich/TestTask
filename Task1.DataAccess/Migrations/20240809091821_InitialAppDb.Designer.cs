﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Task1.DataAccess;

#nullable disable

namespace Task1.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240809091821_InitialAppDb")]
    partial class InitialAppDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Task1.Domain.Entities.Rate", b =>
                {
                    b.Property<int>("Cur_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Cur_ID"));

                    b.Property<string>("Cur_Abbreviation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cur_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("Cur_OfficialRate")
                        .HasColumnType("numeric");

                    b.Property<int>("Cur_Scale")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Cur_ID");

                    b.ToTable("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
