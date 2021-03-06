﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using ShitFood.Api;

namespace ShitFood.Api.Migrations
{
    [DbContext(typeof(ShitFoodContext))]
    [Migration("20191027105430_AddedUpdatedToSources")]
    partial class AddedUpdatedToSources
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
                    b.Property<int>("FHRSID");

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

                    b.Property<double>("Latitude");

                    b.Property<string>("Link");

                    b.Property<string>("LocalAuthorityBusinessID");

                    b.Property<string>("LocalAuthorityCode");

                    b.Property<string>("LocalAuthorityEmailAddress");

                    b.Property<string>("LocalAuthorityName");

                    b.Property<string>("LocalAuthorityWebSite");

                    b.Property<double>("Longitude");

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

                    b.Property<DateTime?>("Updated");

                    b.HasKey("FHRSID");

                    b.HasIndex("PlaceId")
                        .IsUnique()
                        .HasFilter("[PlaceId] IS NOT NULL");

                    b.HasIndex("RatingValue");

                    b.ToTable("FoodHygieneRating");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.GetShitRequestPto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientIpAddress");

                    b.Property<int>("Distance");

                    b.Property<Point>("Location");

                    b.Property<DateTime>("Requested");

                    b.HasKey("Id");

                    b.ToTable("GetShitRequest");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.GooglePlacesPto", b =>
                {
                    b.Property<string>("Id");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<bool>("PermanentlyClosed");

                    b.Property<Guid?>("PlaceId");

                    b.Property<int?>("PriceLevel");

                    b.Property<double>("Rating");

                    b.Property<DateTime?>("Updated");

                    b.Property<int>("UserRatingsTotal");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId")
                        .IsUnique()
                        .HasFilter("[PlaceId] IS NOT NULL");

                    b.HasIndex("Rating");

                    b.ToTable("GooglePlaces");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.PlacePto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Point>("Location");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.TripAdvisorPto", b =>
                {
                    b.Property<int>("LocationId");

                    b.Property<string>("Details");

                    b.Property<Guid?>("PlaceId");

                    b.Property<double>("Rating");

                    b.Property<string>("Summary");

                    b.Property<DateTime?>("Updated");

                    b.HasKey("LocationId");

                    b.HasIndex("PlaceId")
                        .IsUnique()
                        .HasFilter("[PlaceId] IS NOT NULL");

                    b.HasIndex("Rating");

                    b.ToTable("TripAdvisor");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.FoodHygieneRatingPto", b =>
                {
                    b.HasOne("ShitFood.Api.Ptos.PlacePto", "Place")
                        .WithOne("FoodHygieneRating")
                        .HasForeignKey("ShitFood.Api.Ptos.FoodHygieneRatingPto", "PlaceId");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.GooglePlacesPto", b =>
                {
                    b.HasOne("ShitFood.Api.Ptos.PlacePto", "Place")
                        .WithOne("GooglePlaces")
                        .HasForeignKey("ShitFood.Api.Ptos.GooglePlacesPto", "PlaceId");
                });

            modelBuilder.Entity("ShitFood.Api.Ptos.TripAdvisorPto", b =>
                {
                    b.HasOne("ShitFood.Api.Ptos.PlacePto", "Place")
                        .WithOne("TripAdvisorLocation")
                        .HasForeignKey("ShitFood.Api.Ptos.TripAdvisorPto", "PlaceId");
                });
#pragma warning restore 612, 618
        }
    }
}
