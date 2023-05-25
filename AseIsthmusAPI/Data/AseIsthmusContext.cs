using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AseIsthmusAPI.Data;

public partial class AseIsthmusContext : DbContext
{
    public AseIsthmusContext()
    {
    }

    public AseIsthmusContext(DbContextOptions<AseIsthmusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agreement> Agreements { get; set; } 

    public virtual DbSet<Beneficiary> Beneficiaries { get; set; } 

    public virtual DbSet<CategoryAgreement> CategoryAgreements { get; set; } 

    public virtual DbSet<ContributionBalance> ContributionBalances { get; set; }

    public virtual DbSet<ContributionUsage> ContributionUsages { get; set; } 

    public virtual DbSet<LoanBalance> LoanBalances { get; set; } 

    public virtual DbSet<LoanRequest> LoanRequests { get; set; } 

    public virtual DbSet<LoansType> LoansTypes { get; set; } 

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Role> Roles { get; set; } 

    public virtual DbSet<SavingsBalance> SavingsBalances { get; set; } 

    public virtual DbSet<SavingsRequest> SavingsRequests { get; set; } 

    public virtual DbSet<SavingsType> SavingsTypes { get; set; } 

    public virtual DbSet<TransactionLog> TransactionLogs { get; set; } 

    public virtual DbSet<User> Users { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agreement>(entity =>
        {
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.CategoryAgreement).WithMany(p => p.Agreements)
                .HasForeignKey(d => d.CategoryAgreementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agreement_CategoryAgreement");
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.Property(e => e.BeneficiaryName).HasMaxLength(40);
            entity.Property(e => e.BeneficiaryNumberId).HasMaxLength(12);
            entity.Property(e => e.BeneficiaryPercentage).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BeneficiaryRelation).HasMaxLength(20);
            entity.Property(e => e.PersonId).HasMaxLength(12);

            entity.HasOne(d => d.Person).WithMany(p => p.Beneficiaries)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Beneficiary_Person");
        });

        modelBuilder.Entity<CategoryAgreement>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<ContributionBalance>(entity =>
        {
            entity.Property(e => e.DeductedDate).HasColumnType("date");
            entity.Property(e => e.EmployeeContribution).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EmployerContribution).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PersonId).HasMaxLength(12);

            entity.HasOne(d => d.Person).WithMany(p => p.ContributionBalances)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContributionBalance_Person");
        });

        modelBuilder.Entity<ContributionUsage>(entity =>
        {
            entity.ToTable("ContributionUsage");

            entity.Property(e => e.Description).HasMaxLength(40);
        });

        modelBuilder.Entity<LoanBalance>(entity =>
        {
            entity.Property(e => e.BeginningBalance).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CumulativeInterest).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.EndingBalance).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ExtraPayment).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Interest).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InterestRate).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PaymentDate).HasColumnType("date");
            entity.Property(e => e.PersonId)
                .HasMaxLength(12)
                .HasColumnName("PersonID");
            entity.Property(e => e.Principal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ScheduledPayment).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalPayment).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Person).WithMany(p => p.LoanBalances)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoanBalance_Person");

            entity.HasOne(d => d.SavingsRequest).WithMany(p => p.LoanBalances)
                .HasForeignKey(d => d.SavingsRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoanBalance_SavingsRequest");
        });

        modelBuilder.Entity<LoanRequest>(entity =>
        {
            entity.HasKey(e => e.LoanRequestId).HasName("PK_LoanRequest");

            entity.Property(e => e.AmountRequested).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ApprovedDate).HasColumnType("date");
            entity.Property(e => e.BankAccount).HasMaxLength(25);
            entity.Property(e => e.PersonId).HasMaxLength(12);
            entity.Property(e => e.RequestedDate).HasColumnType("date");

            entity.HasOne(d => d.LoansType).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.LoansTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoanRequest_LoansType");

            entity.HasOne(d => d.Person).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoanRequest_Person");
        });

        modelBuilder.Entity<LoansType>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.InterestRate).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PercentageEmployeeCont).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PercentageEmployerCont).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.ContributionUsage).WithMany(p => p.LoansTypes)
                .HasForeignKey(d => d.ContributionUsageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoansType_ContributionUsage");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("Login");

            entity.Property(e => e.PersonId).HasMaxLength(12);
            entity.Property(e => e.Pw).HasMaxLength(300);

            entity.HasOne(d => d.Person).WithMany(p => p.Logins)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Login_Person");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleDescription).HasMaxLength(20);
        });

        modelBuilder.Entity<SavingsBalance>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("history"));

            entity.Property(e => e.LastAmountDeducted).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.LastDeductedDate).HasColumnType("date");
            entity.Property(e => e.PersonId).HasMaxLength(12);

            entity.HasOne(d => d.Person).WithMany(p => p.SavingsBalances)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SavingsBalance_Person");

            entity.HasOne(d => d.SavingsRequest).WithMany(p => p.SavingsBalances)
                .HasForeignKey(d => d.SavingsRequestId)
                .HasConstraintName("FK_SavingsBalance_SavingsRequest");
        });

        modelBuilder.Entity<SavingsRequest>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ApplicationDate).HasColumnType("date");
            entity.Property(e => e.ApprovedDate).HasColumnType("date");
            entity.Property(e => e.PersonId).HasMaxLength(12);

            entity.HasOne(d => d.Person).WithMany(p => p.SavingsRequests)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SavingsRequest_Person");

            entity.HasOne(d => d.SavingsType).WithMany(p => p.SavingsRequests)
                .HasForeignKey(d => d.SavingsTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SavingsRequest_SavingsType");
        });

        modelBuilder.Entity<SavingsType>(entity =>
        {
            entity.Property(e => e.ApplicationDeadline).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<TransactionLog>(entity =>
        {
            entity.Property(e => e.PersonId).HasMaxLength(12);
            entity.Property(e => e.TableName).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Person).WithMany(p => p.TransactionLogs)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionLog_TransactionLog");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.PersonId);

            entity.Property(e => e.PersonId).HasMaxLength(12);
            entity.Property(e => e.Address1).HasMaxLength(150);
            entity.Property(e => e.Address2).HasMaxLength(150);
            entity.Property(e => e.BankAccount).HasMaxLength(25);
            entity.Property(e => e.DateBirth).HasColumnType("date");
            entity.Property(e => e.EmailAddress).HasMaxLength(35);
            entity.Property(e => e.EnrollmentDate).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName1).HasMaxLength(20);
            entity.Property(e => e.LastName2).HasMaxLength(20);
            entity.Property(e => e.Nationality).HasMaxLength(3);
            entity.Property(e => e.NumberId).HasMaxLength(12);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.WorkStartDate).HasColumnType("date");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Person_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
