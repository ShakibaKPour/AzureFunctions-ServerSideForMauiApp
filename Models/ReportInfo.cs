namespace AzureFunction_RepRepair.Models;

public class ReportInfo
{
    public int ReportDetailsId { get; set; }
    public int ObjectId { get; set; }
    public string? OriginalFaultReport { get; set; }
    public string? TranslatedFaultReport { get; set; }
    //public DateTime ReportDate { get; set; }
    public string? SelectedLanguage { get; set; }
    public string? TypeOfReport { get; set; } //or public list<TypeOfReport> typeOfReport {get; set} = new();
}
