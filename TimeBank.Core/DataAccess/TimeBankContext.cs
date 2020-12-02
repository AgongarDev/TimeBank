using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Core.Models;
using TimeBank.Core.DataAccess;

namespace TimeBank.Core.DataAccess
{
    public class TimeBankContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Validation> Validations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Incidence> Incidences { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public TimeBankContext() { }

        public TimeBankContext(DbContextOptions<TimeBankContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring (DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=DESKTOP-BI3GKO5\SQLEXPRESS;
                                Initial Catalog=TimeBankDB;
                                Integrated Security=True;
                                Connect Timeout=30;
                                Encrypt=False;
                                TrustServerCertificate=False;
                                ApplicationIntent=ReadWrite;
                                MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .HasOne(u => u.User)
                .WithMany(p => p.Payments)
                .HasForeignKey(py => py.TBankUserID);

            modelBuilder.Entity<Validation>()
                .HasOne(py => py.Paid)
                .WithOne(v => v.Validation)
                .HasForeignKey<Payment>(py => py.ValidationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Validation>()
                .HasOne(u => u.User)
                .WithMany(v => v.Validations)
                .HasForeignKey(val => val.TBankUserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.user)
                .WithOne(u => u.Address)
                .HasForeignKey<User>(u => u.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.TBankUserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
