﻿// <auto-generated />
using System;
using MainService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MainService.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240506084918_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MainService.models.analysis", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("downVote")
                        .HasColumnType("int");

                    b.Property<string>("itemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("timesClicked")
                        .HasColumnType("int");

                    b.Property<int?>("trailerReach")
                        .HasColumnType("int");

                    b.Property<int?>("upVote")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("analysis");
                });

            modelBuilder.Entity("MainService.models.catagoryModel", b =>
                {
                    b.Property<int>("catagoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("catagoryId"));

                    b.Property<string>("CatagoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("catagoryId");

                    b.ToTable("Catagory");
                });

            modelBuilder.Entity("MainService.models.episodesModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("episodeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("episodeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("episodeNumber")
                        .HasColumnType("int");

                    b.Property<string>("episodeRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("episodeReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("seasonNumber")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("episodes");
                });

            modelBuilder.Entity("MainService.models.itemCatagories", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("catagoryId")
                        .HasColumnType("int");

                    b.Property<string>("itemCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("itemCatagories");
                });

            modelBuilder.Entity("MainService.models.itemLanguages", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("itemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("languageId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("itemLanguages");
                });

            modelBuilder.Entity("MainService.models.languageModel", b =>
                {
                    b.Property<int>("languageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("languageId"));

                    b.Property<string>("languageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("languageId");

                    b.ToTable("languages");
                });

            modelBuilder.Entity("MainService.models.movieModel", b =>
                {
                    b.Property<int>("movieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("movieId"));

                    b.Property<bool?>("isUpcoming")
                        .HasColumnType("bit");

                    b.Property<string>("movieBackPoster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieCast")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("moviePoster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieTrailerURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("movieId");

                    b.ToTable("movieStorage");
                });

            modelBuilder.Entity("MainService.models.seasonsModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("itemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("seasonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("seasonNumber")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("seasons");
                });

            modelBuilder.Entity("MainService.models.seriesModel", b =>
                {
                    b.Property<int>("itemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("itemId"));

                    b.Property<bool?>("isUpcoming")
                        .HasColumnType("bit");

                    b.Property<string>("itemBackPoster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemCast")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemPoster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("itemSeasons")
                        .HasColumnType("int");

                    b.Property<string>("itemTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemTrailerURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("itemId");

                    b.ToTable("series");
                });
#pragma warning restore 612, 618
        }
    }
}