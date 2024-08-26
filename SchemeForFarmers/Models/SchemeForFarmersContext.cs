using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchemeForFarmers.Areas.Admin.Models;
using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Areas.Farmer.Models;

namespace SchemeForFarmers.Models
{
    public partial class SchemeForFarmersContext : DbContext
    {
        public SchemeForFarmersContext()
        {
        }

        public SchemeForFarmersContext(DbContextOptions<SchemeForFarmersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminDetail> AdminDetails { get; set; } = null!;
        public virtual DbSet<BidderDetail> BidderDetails { get; set; } = null!;
        public virtual DbSet<ClaimInsurance> ClaimInsurances { get; set; } = null!;
        public virtual DbSet<CropAuctionDetail> CropAuctionDetails { get; set; } = null!;
        public virtual DbSet<CropBidDetail> CropBidDetails { get; set; } = null!;
        public virtual DbSet<CropInsuranceDetail> CropInsuranceDetails { get; set; } = null!;
        public virtual DbSet<FarmerDetail> FarmerDetails { get; set; } = null!;
        public virtual DbSet<InsuranceDetail> InsuranceDetails { get; set; } = null!;
        public virtual DbSet<LogInView> LogInViews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminDetail>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK_Admin");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UsertypeId)
                    .HasColumnName("usertype_id")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<BidderDetail>(entity =>
            {
                entity.HasKey(e => e.BidderId)
                    .HasName("PK_Bidder");

                entity.Property(e => e.BidderId).HasColumnName("bidder_id");

                entity.Property(e => e.BAadhar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("b_aadhar");

                entity.Property(e => e.BAccountNo)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("b_accountNo");

                entity.Property(e => e.BAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("b_address");

                entity.Property(e => e.BContact)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("b_contact");

                entity.Property(e => e.BIfscCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("b_ifscCode");

                entity.Property(e => e.BLicense)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("b_license");

                entity.Property(e => e.BName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("b_name");

                entity.Property(e => e.BPan)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("b_pan");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UsertypeId)
                    .HasColumnName("usertype_id")
                    .HasDefaultValueSql("((3))");
            });

            modelBuilder.Entity<ClaimInsurance>(entity =>
            {
                entity.HasKey(e => e.ClaimId);

                entity.ToTable("ClaimInsurance");

                entity.Property(e => e.ClaimId).HasColumnName("claim_id");

                entity.Property(e => e.CauseOfLoss)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cause_of_loss");

                entity.Property(e => e.DateOfLoss)
                    .HasColumnType("date")
                    .HasColumnName("date_of_loss");

                entity.Property(e => e.FarmerName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("farmer_name");

                entity.Property(e => e.InsuranceCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("insurance_company");

                entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.Property(e => e.TotalSumInsured)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total_sumInsured");
            });

            modelBuilder.Entity<CropAuctionDetail>(entity =>
            {
                entity.HasKey(e => e.CropId);

                entity.Property(e => e.CropId).HasColumnName("crop_id");

                entity.Property(e => e.BasePrice).HasColumnName("base_price");

                entity.Property(e => e.CropName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crop_name");

                entity.Property(e => e.CropType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crop_type");

                entity.Property(e => e.FarmerId).HasColumnName("farmer_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.Property(e => e.IsSold).HasColumnName("is_sold");

                entity.Property(e => e.Msp).HasColumnName("msp");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.SoilPhCertificate)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("soil_phCertificate");

                entity.Property(e => e.SoldDate)
                    .HasColumnType("date")
                    .HasColumnName("sold_date");

                entity.Property(e => e.SoldPrice).HasColumnName("sold_price");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            });

            modelBuilder.Entity<CropBidDetail>(entity =>
            {
                entity.HasKey(e => e.BidId);

                entity.ToTable("CropBidDetail");

                entity.Property(e => e.BidId).HasColumnName("bid_id");

                entity.Property(e => e.BasePrice).HasColumnName("base_price");

                entity.Property(e => e.Bid).HasColumnName("bid");

                entity.Property(e => e.BidderId).HasColumnName("bidder_id");

                entity.Property(e => e.CropId).HasColumnName("crop_id");

                entity.Property(e => e.CropName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crop_name");

                entity.Property(e => e.CropType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crop_type");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<CropInsuranceDetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ActurialRate)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("acturial_rate");

                entity.Property(e => e.CropName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crop_name");

                entity.Property(e => e.CropType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crop_type");

                entity.Property(e => e.InsuranceCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("insurance_company");

                entity.Property(e => e.Msp).HasColumnName("msp");

                entity.Property(e => e.SharePremium)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("share_premium");

                entity.Property(e => e.SumInsured).HasColumnName("sum_insured");
            });

            modelBuilder.Entity<FarmerDetail>(entity =>
            {
                entity.HasKey(e => e.FarmerId)
                    .HasName("PK_Farmer");

                entity.Property(e => e.FarmerId).HasColumnName("farmer_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FAadhar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("f_aadhar");

                entity.Property(e => e.FAccountNo)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("f_accountNo");

                entity.Property(e => e.FAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("f_address");

                entity.Property(e => e.FCertificate)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("f_certificate");

                entity.Property(e => e.FContact)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("f_contact");

                entity.Property(e => e.FIfscCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("f_ifscCode");

                entity.Property(e => e.FLandAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("f_landAddress");

                entity.Property(e => e.FLandArea).HasColumnName("f_landArea");

                entity.Property(e => e.FName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("f_name");

                entity.Property(e => e.FPan)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("f_pan");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UsertypeId)
                    .HasColumnName("usertype_id")
                    .HasDefaultValueSql("((2))");
            });

            modelBuilder.Entity<InsuranceDetail>(entity =>
            {
                entity.HasKey(e => e.InsuranceId);

                entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.CropName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crop_name");

                entity.Property(e => e.CropType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crop_type");

                entity.Property(e => e.FarmerId).HasColumnName("farmer_id");

                entity.Property(e => e.InsuranceCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("insurance_company");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.Property(e => e.IsClaimed).HasColumnName("is_claimed");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.PremiumAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("premium_amount");

                entity.Property(e => e.SharePremium)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("share_premium");

                entity.Property(e => e.SumInsured).HasColumnName("sum_insured");

                entity.Property(e => e.TotalSumInsured).HasColumnName("total_sumInsured");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<LogInView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LogInView");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UsertypeId).HasColumnName("usertype_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
