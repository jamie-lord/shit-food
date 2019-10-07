﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShitFood.Api;

namespace ShitFood.Api.Migrations
{
    [DbContext(typeof(ShitFoodContext))]
    [Migration("20191007124402_Make-Food-Hygiene-Rating-Optional-Relationship")]
    partial class MakeFoodHygieneRatingOptionalRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShitFood.Api.Ptos.FoodHygieneRatingPto", b =>
                {
                    b.Property<int>("FHRSID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AddressLine3");

                    b.Property<string>("AddressLine4");

                    b.Property<string>("BusinessName");

                    b.Property<string>("BusinessType");

                    b.Property<int>("BusinessTypeID");

                    b.Property<int?>("ConfidenceInManagement");

                    b.Property<double>("Distance");

                    b.Property<int?>("Hygiene");

                    b.Property<string>("Latitude");

                    b.Property<string>("Link");

                    b.Property<string>("LocalAuthorityBusinessID");

                    b.Property<string>("LocalAuthorityCode");

                    b.Property<string>("LocalAuthorityEmailAddress");

                    b.Property<string>("LocalAuthorityName");

                    b.Property<string>("LocalAuthorityWebSite");

                    b.Property<string>("Longitude");

                    b.Property<bool>("NewRatingPending");

                    b.Property<string>("Phone");

                    b.Property<Guid?>("PlaceId");

                    b.Property<string>("PostCode");

                    b.Property<DateTime>("RatingDate");

                    b.Property<string>("RatingKey");

                    b.Property<string>("RatingValue");

                    b.Property<string>("RightToReply");

                    b.Property<string>("SchemeType");

                    b.Property<int?>("Structural");

                    b.HasKey("FHRSID");

                    b.HasIndex("PlaceId")
                        .IsUnique()
                        .HasFilter("[PlaceId] IS NOT NULL");

                    b.ToTable("FoodHygieneRating");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.PlacePto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Lat");

                    b.Property<double>("Lng");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.FoodHygieneRatingPto", b =>
                {
                    b.HasOne("ShitFood.Api.Ptos.PlacePto", "Place")
                        .WithOne("FoodHygieneRating")
                        .HasForeignKey("ShitFood.Api.Ptos.FoodHygieneRatingPto", "PlaceId");
                });
#pragma warning restore 612, 618
        }
    }
}
