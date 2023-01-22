using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorApp6.Shared.Models
{
    public partial class appdbContext : DbContext
    {
        public appdbContext()
        {
        }

        public appdbContext(DbContextOptions<appdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupsItem> GroupsItems { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemsMatterial> ItemsMatterials { get; set; } = null!;
        public virtual DbSet<Matterial> Matterials { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=AppDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Idgroup)
                    .HasName("PKIDGroup");

                entity.ToTable("Groups", "app");

                entity.Property(e => e.Idgroup).HasColumnName("IDGroup");

                entity.Property(e => e.Name)
                    .HasMaxLength(512)
                    .HasColumnName("name");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FKUserGroup");
            });

            modelBuilder.Entity<GroupsItem>(entity =>
            {
                entity.HasKey(e => e.IditemsGroup)
                    .HasName("PKIDItemsGroup");

                entity.ToTable("GroupsItems", "app");

                entity.Property(e => e.IditemsGroup).HasColumnName("IDItemsGroup");

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.NumberIt)
                    .HasColumnName("numberIt")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupsItems)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FKGroupItem");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.GroupsItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FKItemGroup");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Iditem)
                    .HasName("PKIDItem");

                entity.ToTable("Items", "app");

                entity.Property(e => e.Iditem).HasColumnName("IDItem");

                entity.Property(e => e.DangerousColor).HasColumnName("dangerousColor");

                entity.Property(e => e.DateOfPurchase)
                    .HasColumnType("date")
                    .HasColumnName("dateOfPurchase")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(512)
                    .HasColumnName("descriptions");

                entity.Property(e => e.HeightIt).HasColumnName("heightIt");

                entity.Property(e => e.LastWash)
                    .HasColumnType("date")
                    .HasColumnName("lastWash");

                entity.Property(e => e.LengthIt).HasColumnName("lengthIt");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.NrOfWashes)
                    .HasColumnName("nrOfWashes")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.WeightIt).HasColumnName("weightIt");

                entity.Property(e => e.WidthIt).HasColumnName("widthIt");
            });

            modelBuilder.Entity<ItemsMatterial>(entity =>
            {
                entity.HasKey(e => e.IdmaterialsItem)
                    .HasName("PKIDMaterialsItem");

                entity.ToTable("ItemsMatterials", "app");

                entity.Property(e => e.IdmaterialsItem).HasColumnName("IDMaterialsItem");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.Layer).HasColumnName("layer");

                entity.Property(e => e.MatId).HasColumnName("matID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemsMatterials)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKItemMat");

                entity.HasOne(d => d.Mat)
                    .WithMany(p => p.ItemsMatterials)
                    .HasForeignKey(d => d.MatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMatItem");
            });

            modelBuilder.Entity<Matterial>(entity =>
            {
                entity.HasKey(e => e.Idmat)
                    .HasName("PKIDMat");

                entity.ToTable("Matterial", "app");

                entity.Property(e => e.Idmat).HasColumnName("IDMat");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(512)
                    .HasColumnName("descriptions");

                entity.Property(e => e.Materialname)
                    .HasMaxLength(128)
                    .HasColumnName("materialname");

                entity.Property(e => e.TempOfWash)
                    .HasColumnName("tempOfWash")
                    .HasDefaultValueSql("((30))");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Idnotes)
                    .HasName("PKIDNotes");

                entity.ToTable("Notes", "app");

                entity.Property(e => e.Idnotes).HasColumnName("IDNotes");

                entity.Property(e => e.IsCoded).HasColumnName("isCoded");

                entity.Property(e => e.Note1).HasColumnName("note");

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Notes)
                    .UsingEntity<Dictionary<string, object>>(
                        "UsersNote",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKUserNU"),
                        r => r.HasOne<Note>().WithMany().HasForeignKey("NotesId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKNotesNU"),
                        j =>
                        {
                            j.HasKey("NotesId", "UserId").HasName("PKUserNotes");

                            j.ToTable("UsersNotes", "app");

                            j.IndexerProperty<int>("NotesId").HasColumnName("NotesID");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PKIDUser");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Email)
                    .HasMaxLength(512)
                    .HasColumnName("email");

                entity.Property(e => e.Pass)
                    .HasMaxLength(512)
                    .HasColumnName("pass");

                entity.Property(e => e.Userlogin)
                    .HasMaxLength(512)
                    .HasColumnName("userlogin");

                entity.Property(e => e.Username)
                    .HasMaxLength(128)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
