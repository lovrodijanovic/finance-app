using FinanceApp.Server.Models.Definitions;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options):
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }

    public DbSet<User> User { get; set; }

    public DbSet<Debt> Debt { get; set; }

    public DbSet<DebtType> DebtType { get; set; }

    public DbSet<EmergencyFund> EmergencyFund { get; set; }

    public DbSet<FinancialStatus> FinancialStatus { get; set; }

    public DbSet<Investment> Investment { get; set; }

    public DbSet<InvestmentType> InvestmentType { get; set; }

    public DbSet<VoluntaryPensionInsurance> VoluntaryPensionInsurance { get; set; }
}
