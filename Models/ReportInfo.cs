namespace AzureFunction_RepRepair.Models;

public class ReportInfo
{
    public Guid ID { get; set; }
    public Guid QrCode { get; set; }
    public string? OriginalFaultReport { get; set; }
    public string? TranslatedFaultReport { get; set; }
    public Guid SelectedLanguage { get; set; }
    public Guid TypeOfReport { get; set; }
}
