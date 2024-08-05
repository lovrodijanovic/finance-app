namespace FinanceApp.Server.Models.Definitions;

public class DebtType : BaseEntity
{
    public string Name { get; set; } = "";

    public string CroatianName { get; set; } = "";

    public string Description { get; set; } = "";

    public string CroatianDescription { get; set; } = "";

    public string Abbreviation { get; set; } = "";

    public int SortOrder { get; set; }
}
