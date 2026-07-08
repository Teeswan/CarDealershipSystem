using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarBrand> CarBrands { get; set; }

    public virtual DbSet<CarDetail> CarDetails { get; set; }

    public virtual DbSet<CarImage> CarImages { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Appointmentid).HasName("PK__Appointm__D0666126B77A9D06");

            entity.HasIndex(e => e.Appointmentcode, "UQ__Appointm__D397B1212F25FC31").IsUnique();

            entity.Property(e => e.Appointmentid).HasColumnName("appointmentid");
            entity.Property(e => e.Appointmentcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("appointmentcode");
            entity.Property(e => e.Appointmentdatetime)
                .HasColumnType("datetime")
                .HasColumnName("appointmentdatetime");
            entity.Property(e => e.Appointmenttype)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Viewing")
                .HasColumnName("appointmenttype");
            entity.Property(e => e.Carid).HasColumnName("carid");
            entity.Property(e => e.CustomerContact)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_contact");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Scheduled")
                .HasColumnName("status");

            entity.HasOne(d => d.Car).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Carid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Cars");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Carid).HasName("PK__Cars__1439FCACBF4E9BA5");

            entity.Property(e => e.Carid).HasColumnName("carid");
            entity.Property(e => e.Carbrandid).HasColumnName("carbrandid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Modelnumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modelnumber");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Available")
                .HasColumnName("status");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Carbrand).WithMany(p => p.Cars)
                .HasForeignKey(d => d.Carbrandid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_CarBrands");

            entity.HasOne(d => d.Category).WithMany(p => p.Cars)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_Categories");

            entity.HasMany(d => d.Features).WithMany(p => p.Cars)
                .UsingEntity<Dictionary<string, object>>(
                    "CarFeature",
                    r => r.HasOne<Feature>().WithMany()
                        .HasForeignKey("Featureid")
                        .HasConstraintName("FK_CarFeatures_Features"),
                    l => l.HasOne<Car>().WithMany()
                        .HasForeignKey("Carid")
                        .HasConstraintName("FK_CarFeatures_Cars"),
                    j =>
                    {
                        j.HasKey("Carid", "Featureid").HasName("PK__CarFeatu__BB769C6784E8F728");
                        j.ToTable("CarFeatures");
                        j.IndexerProperty<int>("Carid").HasColumnName("carid");
                        j.IndexerProperty<int>("Featureid").HasColumnName("featureid");
                    });
        });

        modelBuilder.Entity<CarBrand>(entity =>
        {
            entity.HasKey(e => e.Carbrandid).HasName("PK__CarBrand__2DD3E8946454A9CB");

            entity.HasIndex(e => e.Carbrandname, "UQ__CarBrand__EE27CFB8DA7999A9").IsUnique();

            entity.Property(e => e.Carbrandid).HasColumnName("carbrandid");
            entity.Property(e => e.Carbrandname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("carbrandname");
        });

        modelBuilder.Entity<CarDetail>(entity =>
        {
            entity.HasKey(e => e.Carid).HasName("PK__CarDetai__1439FCAC76F882ED");

            entity.HasIndex(e => e.Vinnumber, "UQ__CarDetai__AF6DED1BFEDA9C7B").IsUnique();

            entity.Property(e => e.Carid)
                .ValueGeneratedNever()
                .HasColumnName("carid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Enginetype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("enginetype");
            entity.Property(e => e.Exteriorcolor)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("exteriorcolor");
            entity.Property(e => e.Fueltype)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fueltype");
            entity.Property(e => e.Interiorcolor)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("interiorcolor");
            entity.Property(e => e.Mileage).HasColumnName("mileage");
            entity.Property(e => e.Numberofowners)
                .HasDefaultValue(0)
                .HasColumnName("numberofowners");
            entity.Property(e => e.Transmission)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("transmission");
            entity.Property(e => e.Vinnumber)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("vinnumber");

            entity.HasOne(d => d.Car).WithOne(p => p.CarDetail)
                .HasForeignKey<CarDetail>(d => d.Carid)
                .HasConstraintName("FK_CarDetails_Cars");
        });

        modelBuilder.Entity<CarImage>(entity =>
        {
            entity.HasKey(e => e.Imageid).HasName("PK__CarImage__336F9F7D66A80EFD");

            entity.Property(e => e.Imageid).HasColumnName("imageid");
            entity.Property(e => e.Carid).HasColumnName("carid");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("imageurl");
            entity.Property(e => e.Isprimary).HasColumnName("isprimary");

            entity.HasOne(d => d.Car).WithMany(p => p.CarImages)
                .HasForeignKey(d => d.Carid)
                .HasConstraintName("FK_CarImages_Cars");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("PK__Categori__23CDE5905A205E20");

            entity.HasIndex(e => e.Categoryname, "UQ__Categori__1A0D12CE2C5ADAA6").IsUnique();

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoryname");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.Featureid).HasName("PK__Features__F4F60CBBE56A9513");

            entity.HasIndex(e => e.Featurename, "UQ__Features__8335B5A52EC69937").IsUnique();

            entity.Property(e => e.Featureid).HasColumnName("featureid");
            entity.Property(e => e.Featurename)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("featurename");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("PK__Payments__AF26EBEE44034DE9");

            entity.Property(e => e.Paymentid).HasColumnName("paymentid");
            entity.Property(e => e.Amountpaid)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("amountpaid");
            entity.Property(e => e.Paymentdatetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("paymentdatetime");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paymentmethod");
            entity.Property(e => e.Paymentstatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("paymentstatus");
            entity.Property(e => e.Saleid).HasColumnName("saleid");

            entity.HasOne(d => d.Sale).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Saleid)
                .HasConstraintName("FK_Payments_Sales");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Saleid).HasName("PK__Sales__FAEFF92D9F949AD2");

            entity.HasIndex(e => e.Carid, "UQ__Sales__1439FCAD5E048AF1").IsUnique();

            entity.HasIndex(e => e.Salecode, "UQ__Sales__FC59B55CA20D3FB1").IsUnique();

            entity.Property(e => e.Saleid).HasColumnName("saleid");
            entity.Property(e => e.Carid).HasColumnName("carid");
            entity.Property(e => e.CustomerContact)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_contact");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Salecode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("salecode");
            entity.Property(e => e.Saledatetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("saledatetime");
            entity.Property(e => e.Saleprice)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("saleprice");

            entity.HasOne(d => d.Car).WithOne(p => p.Sale)
                .HasForeignKey<Sale>(d => d.Carid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Cars");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
