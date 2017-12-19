﻿// <auto-generated />
using ECApiRT.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ECApiRT.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20171204112828_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ECApiRT.Models.cart", b =>
                {
                    b.Property<int>("cartID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<int>("count");

                    b.Property<int>("productID");

                    b.Property<string>("productName");

                    b.HasKey("cartID");

                    b.ToTable("cart");
                });

            modelBuilder.Entity("ECApiRT.Models.Product", b =>
                {
                    b.Property<int>("productID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<string>("imageUrl");

                    b.Property<int>("price");

                    b.Property<string>("prodUrl");

                    b.Property<string>("productCode");

                    b.Property<string>("productName");

                    b.Property<string>("productType");

                    b.Property<string>("releaseDate");

                    b.Property<int>("starRating");

                    b.HasKey("productID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ECApiRT.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EMail");

                    b.Property<string>("Privilege");

                    b.Property<string>("UserLName");

                    b.Property<string>("UserName");

                    b.Property<string>("UserPassword");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
