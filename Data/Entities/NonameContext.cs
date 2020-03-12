using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace noname.Data.Entities
{
    public partial class NonameContext : DbContext
    {
        public NonameContext()
        {
        }

        public NonameContext(DbContextOptions<NonameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CompanyType> CompanyType { get; set; }
        public virtual DbSet<Entry> Entry { get; set; }
        public virtual DbSet<EntryLike> EntryLike { get; set; }
        public virtual DbSet<EstateType> EstateType { get; set; }
        public virtual DbSet<GenderType> GenderType { get; set; }
        public virtual DbSet<Header> Header { get; set; }
        public virtual DbSet<HeatingType> HeatingType { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostAdvertisement> PostAdvertisement { get; set; }
        public virtual DbSet<PostAdvertisementEstate> PostAdvertisementEstate { get; set; }
        public virtual DbSet<PostAdvertisementEvent> PostAdvertisementEvent { get; set; }
        public virtual DbSet<PostAdvertisementJob> PostAdvertisementJob { get; set; }
        public virtual DbSet<PostAdvertisementResidence> PostAdvertisementResidence { get; set; }
        public virtual DbSet<PostAdvertisementStuff> PostAdvertisementStuff { get; set; }
        public virtual DbSet<PostComment> PostComment { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<PostFavorite> PostFavorite { get; set; }
        public virtual DbSet<PostImage> PostImage { get; set; }
        public virtual DbSet<PostLike> PostLike { get; set; }
        public virtual DbSet<ResidenceType> ResidenceType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<StuffCategory> StuffCategory { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCompany> UserCompany { get; set; }
        public virtual DbSet<UserContact> UsserContact { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Sector> Sector { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;Database=noname;User=root;Password=;TreatTinyAsBoolean=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryOrder)
                    .HasColumnName("categoryOrder")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UpperCategoryId)
                    .HasColumnName("upperCategoryId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.ToTable("company_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("sector");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<GenderType>(entity =>
            {
                entity.ToTable("gender_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<StatusType>(entity =>
            {
                entity.ToTable("statustype");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("district");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
                entity.Property(e => e.CityId)
                    .IsRequired()
                    .HasColumnName("cityId")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<Entry>(entity =>
            {
                entity.ToTable("entry");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Body)
                    .HasColumnName("body")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditDate)
                    .HasColumnName("editDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.HeaderId)
                    .HasColumnName("headerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<EntryLike>(entity =>
            {
                entity.ToTable("entry_like");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("date");

                entity.Property(e => e.EntryId)
                    .HasColumnName("entryId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<EstateType>(entity =>
            {
                entity.ToTable("estate_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Header>(entity =>
            {
                entity.ToTable("header");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<HeatingType>(entity =>
            {
                entity.ToTable("heating_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .HasColumnType("varchar(5000)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditDate)
                    .HasColumnName("editDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<PostAdvertisement>(entity =>
            {
                entity.ToTable("post_advertisement");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("categoryId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasColumnName("header")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.PostId)
                    .HasColumnName("postId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<PostAdvertisementEstate>(entity =>
            {
                entity.ToTable("post_advertisement_estate");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.CityId)
                    .HasColumnName("cityId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("districtId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dues)
                    .HasColumnName("dues")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.EstateTypeId)
                    .HasColumnName("estateTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HeatingTypeId)
                    .HasColumnName("heatingTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsFurnished)
                    .HasColumnName("isFurnished")
                    .HasColumnType("tinyint(3)");

                entity.Property(e => e.PostAdvertisementId)
                    .HasColumnName("postAdvertisementId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.RoomTypeId)
                    .HasColumnName("roomTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MapLat)
                    .HasColumnName("mapLat")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.MapLong)
                    .HasColumnName("mapLong")
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<PostAdvertisementEvent>(entity =>
            {
                entity.ToTable("post_advertisement_event");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DoorOpenDate)
                    .HasColumnName("doorOpenDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasColumnName("endDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocationId)
                    .HasColumnName("locationId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PostAdvertisementId)
                    .HasColumnName("postAdvertisementId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PostAdvertisementJob>(entity =>
            {
                entity.ToTable("post_advertisement_job");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("cityId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("districtId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PostAdvertisementId)
                    .HasColumnName("postAdvertisementId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SectorId)
                    .HasColumnName("sectorId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<PostAdvertisementResidence>(entity =>
            {
                entity.ToTable("post_advertisement_residence");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("cityId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("districtId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GenderTypeId)
                    .HasColumnName("genderTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PersonCount)
                    .HasColumnName("personCount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PostAdvertisementId)
                    .HasColumnName("postAdvertisementId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.ResidenceTypeId)
                    .HasColumnName("residenceTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MapLat)
                    .HasColumnName("mapLat")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.MapLong)
                    .HasColumnName("mapLong")
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<PostAdvertisementStuff>(entity =>
            {
                entity.ToTable("post_advertisement_stuff");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PostAdvertisementId)
                    .HasColumnName("postAdvertisementId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.StatusTypeId)
                    .HasColumnName("statusTypeId")
                    .HasColumnType("tinyint(3)");

                entity.Property(e => e.StuffCategoryId)
                    .HasColumnName("stuffCategoryId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.ToTable("post_comment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditDate)
                    .HasColumnName("editDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostId)
                    .HasColumnName("postId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<PostFavorite>(entity =>
            {
                entity.ToTable("post_favorite");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostId)
                    .HasColumnName("postId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<PostImage>(entity =>
            {
                entity.ToTable("post_image");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("imageUrl")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.PostId)
                    .HasColumnName("postId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<PostLike>(entity =>
            {
                entity.ToTable("post_like");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostId)
                    .HasColumnName("postId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ResidenceType>(entity =>
            {
                entity.ToTable("residence_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("room_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<StuffCategory>(entity =>
            {
                entity.ToTable("stuff_category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryOrder)
                    .HasColumnName("categoryOrder")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.UpperCategoryId)
                    .HasColumnName("upperCategoryId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.About)             
                    .HasColumnName("about")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.ActivationCode)
                    .IsRequired()
                    .HasColumnName("activationCode")
                    .HasColumnType("varchar(28)");

                entity.Property(e => e.CityId)
                    .HasColumnName("cityId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("districtId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("imageUrl")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(3)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(20)");
                entity.Property(e=>e.isCompany)
                    .IsRequired()
                    .HasColumnName("isCompany")
                    .HasColumnType("tinyint(3)");

            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.ToTable("user_company");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.CompanyTypeId)
                    .HasColumnName("companyTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<UserContact>(entity =>
            {
                entity.ToTable("usser_contact");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Facebook)
                    .HasColumnName("facebook")
                    .HasColumnType("int(100)");

                entity.Property(e => e.Gsm)
                    .HasColumnName("gsm")
                    .HasColumnType("int(20)");

                entity.Property(e => e.Instagram)
                    .HasColumnName("instagram")
                    .HasColumnType("int(100)");

                entity.Property(e => e.Linkedin)
                    .HasColumnName("linkedin")
                    .HasColumnType("int(100)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("int(20)");

                entity.Property(e => e.Twitter)
                    .HasColumnName("twitter")
                    .HasColumnType("int(100)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasColumnType("int(100)");
            });
            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.ToTable("system_log");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("varchar(1000)");



                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasColumnName("entityName")
                    .HasColumnType("varchar(50)");



                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });
        }
    }
}
