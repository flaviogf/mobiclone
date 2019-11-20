using Microsoft.EntityFrameworkCore;
using Mobiclone.Api.Models;

namespace Mobiclone.Api
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
    }
}
