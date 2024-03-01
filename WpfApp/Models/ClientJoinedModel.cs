using System.Windows;

namespace WpfApp.Models;

public class ClientJoinedModel 
{ 
    public int ClientId { get; set; }
    public String ClientName { get; set; }
    public String ClientForename { get; set; }
    public String ClientPhoneNumber { get; set; }
    public int CouchId { get; set; }
    public String CouchName { get; set; }
    public decimal CouchSalary { get; set; }
    public String CouchPhoneNumber { get; set; }
}