namespace WpfApp.Models;

public class AccountingModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SubscriptionId { get; set; }
    public DateTime Month { get; set; }
}