using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Models;

namespace Mobiclone.Api.Database
{
    public class MobicloneContext : DbContext
    {
        public MobicloneContext(DbContextOptions<MobicloneContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Revenue> Revenues { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Output> Outputs { get; set; }

        public DbSet<Input> Inputs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Revenue>()
                .HasOne(it => it.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Expense>()
                .HasOne(it => it.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Output>()
                .HasOne(it => it.To)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Output>()
                .HasOne(it => it.From)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Input>()
                .HasOne(it => it.To)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Input>()
                .HasOne(it => it.From)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
