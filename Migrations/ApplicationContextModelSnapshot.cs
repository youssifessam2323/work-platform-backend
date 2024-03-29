﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using work_platform_backend.Models;

namespace work_platform_backend.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("work_platform_backend.Models.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TaskId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("work_platform_backend.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("MessageTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("MessageTypeId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("work_platform_backend.Models.ChatMessageType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MessageTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatMessageTypes");
                });

            modelBuilder.Entity("work_platform_backend.Models.CheckPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CheckpointText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentRTaskId")
                        .HasColumnType("int");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentRTaskId");

                    b.ToTable("CheckPoints");
                });

            modelBuilder.Entity("work_platform_backend.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("work_platform_backend.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("work_platform_backend.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActualEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ActualStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PlannedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlannedStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("RoomId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("work_platform_backend.Models.ProjectManager", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("ProjectManagers");
                });

            modelBuilder.Entity("work_platform_backend.Models.RTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ActualEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ActualStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("DependantTaskId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCheckPointId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PlannedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlannedStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DependantTaskId");

                    b.HasIndex("ParentCheckPointId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("work_platform_backend.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("work_platform_backend.Models.RoomSettings", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("SettingId")
                        .HasColumnType("int");

                    b.HasKey("RoomId", "SettingId");

                    b.HasIndex("SettingId");

                    b.ToTable("RoomSettings");
                });

            modelBuilder.Entity("work_platform_backend.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExtraTime")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("TaskProgress")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("work_platform_backend.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("work_platform_backend.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("IsActiveTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeaderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentTeamId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<Guid>("TeamCode")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.HasIndex("ParentTeamId");

                    b.HasIndex("RoomId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("work_platform_backend.Models.TeamChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChatName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TeamId")
                        .IsUnique()
                        .HasFilter("[TeamId] IS NOT NULL");

                    b.ToTable("TeamChats");
                });

            modelBuilder.Entity("work_platform_backend.Models.TeamProject", b =>
                {
                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("TeamId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("TeamProjects");
                });

            modelBuilder.Entity("work_platform_backend.Models.TeamsMembers", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamsMembers");
                });

            modelBuilder.Entity("work_platform_backend.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("work_platform_backend.Models.UserTask", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("UserTasks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("work_platform_backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("work_platform_backend.Models.Attachment", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("work_platform_backend.Models.RTask", "Task")
                        .WithMany("Attachments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("work_platform_backend.Models.ChatMessage", b =>
                {
                    b.HasOne("work_platform_backend.Models.TeamChat", "Chat")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("work_platform_backend.Models.User", "Creator")
                        .WithMany("Messages")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("work_platform_backend.Models.ChatMessageType", "MessageType")
                        .WithMany()
                        .HasForeignKey("MessageTypeId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("work_platform_backend.Models.CheckPoint", b =>
                {
                    b.HasOne("work_platform_backend.Models.RTask", "ParentRTask")
                        .WithMany("ChildCheckPoints")
                        .HasForeignKey("ParentRTaskId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("work_platform_backend.Models.Comment", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", "Creator")
                        .WithMany("Comments")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("work_platform_backend.Models.Comment", null)
                        .WithMany("Replies")
                        .HasForeignKey("ParentCommentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("work_platform_backend.Models.RTask", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("work_platform_backend.Models.Notification", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("work_platform_backend.Models.Project", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", "Creator")
                        .WithMany("OwnedProjects")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("work_platform_backend.Models.Room", "Room")
                        .WithMany("Projects")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("work_platform_backend.Models.ProjectManager", b =>
                {
                    b.HasOne("work_platform_backend.Models.Room", "Room")
                        .WithMany("ProjectManagers")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("work_platform_backend.Models.User", "User")
                        .WithMany("ManagedProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("work_platform_backend.Models.RTask", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", "Creator")
                        .WithMany("OwnedTasks")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("work_platform_backend.Models.RTask", null)
                        .WithMany("DependantTasks")
                        .HasForeignKey("DependantTaskId");

                    b.HasOne("work_platform_backend.Models.CheckPoint", "ParentCheckPoint")
                        .WithMany("SubTasks")
                        .HasForeignKey("ParentCheckPointId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("work_platform_backend.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("work_platform_backend.Models.Team", "Team")
                        .WithMany("Tasks")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("work_platform_backend.Models.Room", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", "Creator")
                        .WithMany("Rooms")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("work_platform_backend.Models.RoomSettings", b =>
                {
                    b.HasOne("work_platform_backend.Models.Room", "Room")
                        .WithMany("RoomSettings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("work_platform_backend.Models.Setting", "Setting")
                        .WithMany("RoomSettings")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("work_platform_backend.Models.Session", b =>
                {
                    b.HasOne("work_platform_backend.Models.RTask", "Task")
                        .WithMany("Sessions")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("work_platform_backend.Models.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("work_platform_backend.Models.Team", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", "Leader")
                        .WithMany("Leads")
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("work_platform_backend.Models.Team", "ParentTeam")
                        .WithMany("SubTeams")
                        .HasForeignKey("ParentTeamId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("work_platform_backend.Models.Room", "Room")
                        .WithMany("Teams")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("work_platform_backend.Models.TeamChat", b =>
                {
                    b.HasOne("work_platform_backend.Models.User", "Creator")
                        .WithMany("teamChats")
                        .HasForeignKey("CreatorId");

                    b.HasOne("work_platform_backend.Models.Team", "Team")
                        .WithOne("TeamChat")
                        .HasForeignKey("work_platform_backend.Models.TeamChat", "TeamId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("work_platform_backend.Models.TeamProject", b =>
                {
                    b.HasOne("work_platform_backend.Models.Project", "Project")
                        .WithMany("TeamProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("work_platform_backend.Models.Team", "Team")
                        .WithMany("TeamProjects")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("work_platform_backend.Models.TeamsMembers", b =>
                {
                    b.HasOne("work_platform_backend.Models.Team", "Team")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("work_platform_backend.Models.User", "User")
                        .WithMany("TeamMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("work_platform_backend.Models.UserTask", b =>
                {
                    b.HasOne("work_platform_backend.Models.RTask", "Task")
                        .WithMany("UserTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("work_platform_backend.Models.User", "User")
                        .WithMany("UserTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
