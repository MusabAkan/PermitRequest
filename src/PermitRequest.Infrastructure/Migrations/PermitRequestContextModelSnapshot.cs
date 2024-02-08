﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PermitRequest.Infrastructure.Contexts;

#nullable disable

namespace PermitRequest.Infrastructure.Migrations
{
    [DbContext(typeof(PermitRequestContext))]
    partial class PermitRequestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PermitRequest.Domain.Entities.AdUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AdUsers", (string)null);
                });

            modelBuilder.Entity("PermitRequest.Domain.Entities.CumulativeLeaveRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LeaveTypeId")
                        .HasColumnType("int");

                    b.Property<int>("TotalHours")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CumulativeLeaveRequests", (string)null);
                });

            modelBuilder.Entity("PermitRequest.Domain.Entities.LeaveRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssignedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("FormNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("FormNumber"));

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LeaveType")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestNumber")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("CONCAT('LRF-', FORMAT(FormNumber, 'D6'))");

                    b.Property<int>("WorkflowStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("LeaveRequests", (string)null);
                });

            modelBuilder.Entity("PermitRequest.Domain.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CumulativeLeaveRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CumulativeLeaveRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications", (string)null);
                });

            modelBuilder.Entity("PermitRequest.Domain.Entities.CumulativeLeaveRequest", b =>
                {
                    b.HasOne("PermitRequest.Domain.Entities.AdUser", "User")
                        .WithMany("CumulativeLeaveRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PermitRequest.Domain.Entities.LeaveRequest", b =>
                {
                    b.HasOne("PermitRequest.Domain.Entities.AdUser", "CreatedBy")
                        .WithMany("LeaveRequests")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("PermitRequest.Domain.Entities.LeaveRequest.BetweenDates#PermitRequest.Domain.ValueObjets.BetweenDate", "BetweenDates", b1 =>
                        {
                            b1.Property<Guid>("LeaveRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("StartDate");

                            b1.HasKey("LeaveRequestId");

                            b1.ToTable("LeaveRequests", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("LeaveRequestId");
                        });

                    b.Navigation("BetweenDates")
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("PermitRequest.Domain.Entities.Notification", b =>
                {
                    b.HasOne("PermitRequest.Domain.Entities.CumulativeLeaveRequest", "CumulativeLeaveRequest")
                        .WithMany("Notifications")
                        .HasForeignKey("CumulativeLeaveRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PermitRequest.Domain.Entities.AdUser", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CumulativeLeaveRequest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PermitRequest.Domain.Entities.AdUser", b =>
                {
                    b.Navigation("CumulativeLeaveRequests");

                    b.Navigation("LeaveRequests");

                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("PermitRequest.Domain.Entities.CumulativeLeaveRequest", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
