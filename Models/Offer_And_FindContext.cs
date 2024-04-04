using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OfferAndFindAPI.Models
{
    public partial class Offer_And_FindContext : DbContext
    {
        public Offer_And_FindContext()
        {
        }

        public Offer_And_FindContext(DbContextOptions<Offer_And_FindContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-99H3564\\CRABIBARA;Initial Catalog=Offer_And_Find;User ID=sa;Password=123");
            }
        }

        public virtual DbSet<Ad> Ads { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<RoleUser> RoleUsers { get; set; } = null!;
        public virtual DbSet<StatusAd> StatusAds { get; set; } = null!;
        public virtual DbSet<StatusUser> StatusUsers { get; set; } = null!;
        public virtual DbSet<TypeAd> TypeAds { get; set; } = null!;
        public virtual DbSet<TypeMessage> TypeMessages { get; set; } = null!;
        public virtual DbSet<TypeWork> TypeWorks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>(entity =>
            {
                entity.HasKey(e => e.IdAd)
                    .HasName("PK_8");

                entity.ToTable("Ad");

                entity.HasIndex(e => e.IdType, "FK_2");

                entity.HasIndex(e => e.IdType1, "FK_3");

                entity.HasIndex(e => e.IdStatus, "FK_4");

                entity.HasIndex(e => e.IdUser, "FK_5");

                entity.Property(e => e.IdAd).HasColumnName("ID_Ad");

                entity.Property(e => e.Header)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.IdType).HasColumnName("ID_Type");

                entity.Property(e => e.IdType1).HasColumnName("ID_Type_1");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(e => e.IdChat)
                    .HasName("PK_9");

                entity.ToTable("Chat");

                entity.HasIndex(e => e.IdUser, "FK_2");

                entity.HasIndex(e => e.IdAd, "FK_3");

                entity.Property(e => e.IdChat).HasColumnName("ID_Chat");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.User2).HasColumnName("User_2");

                entity.Property(e => e.IdAd).HasColumnName("ID_Ad");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.IdMessage)
                    .HasName("PK_10");

                entity.ToTable("Message");

                entity.HasIndex(e => e.IdType, "FK_2");

                entity.HasIndex(e => e.IdChat, "FK_3");

                entity.HasIndex(e => e.IdAd, "FK_4");

                entity.Property(e => e.IdMessage).HasColumnName("ID_Message");

                entity.Property(e => e.IdAd).HasColumnName("ID_Ad");

                entity.Property(e => e.IdChat).HasColumnName("ID_Chat");

                entity.Property(e => e.IdType).HasColumnName("ID_Type");

                entity.Property(e => e.Text)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK_2");

                entity.ToTable("Role_User");

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");
            });

            modelBuilder.Entity<StatusAd>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PK_5");

                entity.ToTable("Status_Ad");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.NameStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Status");
            });

            modelBuilder.Entity<StatusUser>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PK_1");

                entity.ToTable("Status_User");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.NameStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Status");
            });

            modelBuilder.Entity<TypeAd>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_3");

                entity.ToTable("Type_Ad");

                entity.Property(e => e.IdType).HasColumnName("ID_Type");

                entity.Property(e => e.NameType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Type");
            });

            modelBuilder.Entity<TypeMessage>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_6");

                entity.ToTable("Type_Message");

                entity.Property(e => e.IdType).HasColumnName("ID_Type");

                entity.Property(e => e.NameType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Type");
            });

            modelBuilder.Entity<TypeWork>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_4");

                entity.ToTable("Type_Work");

                entity.Property(e => e.IdType).HasColumnName("ID_Type");

                entity.Property(e => e.NameType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_7");

                entity.ToTable("User");

                entity.HasIndex(e => e.IdStatus, "FK_2");

                entity.HasIndex(e => e.IdRole, "FK_3");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("E-Mail");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
