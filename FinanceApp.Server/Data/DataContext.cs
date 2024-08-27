using FinanceApp.Server.Models.Definitions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Server.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options):
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseSerialColumns();

        modelBuilder.HasDefaultSchema("identity");
    }

    public DbSet<User> User { get; set; }

    public DbSet<Debt> Debt { get; set; }

    public DbSet<EmergencyFund> EmergencyFund { get; set; }

    public DbSet<FinancialStatus> FinancialStatus { get; set; }

    public DbSet<Investment> Investment { get; set; }

    public DbSet<InvestmentType> InvestmentType { get; set; }

    public DbSet<VoluntaryPensionInsurance> VoluntaryPensionInsurance { get; set; }
}
