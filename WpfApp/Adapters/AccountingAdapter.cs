using System.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using WpfApp.Config;
using WpfApp.Const;
using WpfApp.Models;

namespace WpfApp.Adapters;

public static class AccountingAdapter
{
    private static string  _selectSql = $"SELECT * FROM {DatabaseConst.TABLE_ACCOUNTING}";
    
    
    public static List<AccountingModel> LoadAccountings()
    {
        
        OleDbConnection myConn = new OleDbConnection(AppConfig.DATABASE_CONNECTION_STRING);  
        OleDbDataAdapter myCmd = new OleDbDataAdapter(_selectSql, myConn);  
        myConn.Open();  
        DataSet dtSet = new DataSet();  
        myCmd.Fill(dtSet, DatabaseConst.TABLE_ACCOUNTING);


        List<AccountingModel> result = new List<AccountingModel>();
        foreach (DataRow row in dtSet.Tables[0].Rows)
        {

            result.Add(
                new AccountingModel
                {
                    Id = Convert.ToInt32(row[DatabaseConst.ACCOUNTING_ID]),
                    ClientId = Convert.ToInt32(row[DatabaseConst.CLIENT_ID]),
                    SubscriptionId = Convert.ToInt32(row[DatabaseConst.SUBSCRIPTION_ID]),
                    Month = Convert.ToDateTime(row[DatabaseConst.ACCOUNTING_MOUNTH]),
                });
        }

        return result;
    }
    
}