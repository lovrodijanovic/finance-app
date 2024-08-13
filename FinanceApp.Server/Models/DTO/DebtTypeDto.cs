namespace FinanceApp.Server.Models.DTO;

public class DebtTypeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string CroatianName { get; set; } = "";

    public string Description { get; set; } = "";

    public string CroatianDescription { get; set; } = "";

    public string Abbreviation { get; set; } = "";
}
