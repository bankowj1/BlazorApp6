using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemsGroup> ItemsGroups { get; set; } = null!;
        public virtual DbSet<MaterialsItem> MaterialsItems { get; set; } = null!;
        public virtual DbSet<Matterial> Matterials { get; set; } = null!;
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

                entity.ToTable("groups", "app");

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

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Iditem)
                    .HasName("PKIDItem");

                entity.ToTable("items", "app");

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

            modelBuilder.Entity<ItemsGroup>(entity =>
            {
                entity.HasKey(e => e.IditemsGroup)
                    .HasName("PKIDItemsGroup");

                entity.ToTable("ItemsGroup", "app");

                entity.Property(e => e.IditemsGroup).HasColumnName("IDItemsGroup");

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.NumberIt)
                    .HasColumnName("numberIt")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ItemsGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FKGroupItem");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemsGroups)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FKItemGroup");
            });

            modelBuilder.Entity<MaterialsItem>(entity =>
            {
                entity.HasKey(e => e.IdmaterialsItem)
                    .HasName("PKIDMaterialsItem");

                entity.ToTable("MaterialsItem", "app");

                entity.Property(e => e.IdmaterialsItem).HasColumnName("IDMaterialsItem");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.Layer).HasColumnName("layer");

                entity.Property(e => e.MatId).HasColumnName("matID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.MaterialsItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKItemMat");

                entity.HasOne(d => d.Mat)
                    .WithMany(p => p.MaterialsItems)
                    .HasForeignKey(d => d.MatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMatItem");
            });

            modelBuilder.Entity<Matterial>(entity =>
            {
                entity.HasKey(e => e.Idmat)
                    .HasName("PKIDMat");

                entity.ToTable("matterial", "app");

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

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PKIDUser");

                entity.ToTable("users");

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