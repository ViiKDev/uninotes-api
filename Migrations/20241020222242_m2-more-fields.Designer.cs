﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniNotesAPI.Models;

#nullable disable

namespace UniNotesAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20241020222242_m2-more-fields")]
    partial class m2morefields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("UniNotesAPI.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FolderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Subtitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.HasIndex("UserId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("UniNotesAPI.Models.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("UniNotesAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UniNotesAPI.Models.Document", b =>
                {
                    b.HasOne("UniNotesAPI.Models.Folder", "Folder")
                        .WithMany("Documents")
                        .HasForeignKey("FolderId");

                    b.HasOne("UniNotesAPI.Models.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UniNotesAPI.Models.Folder", b =>
                {
                    b.HasOne("UniNotesAPI.Models.User", "User")
                        .WithMany("Folders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UniNotesAPI.Models.Folder", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("UniNotesAPI.Models.User", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Folders");
                });
#pragma warning restore 612, 618
        }
    }
}
