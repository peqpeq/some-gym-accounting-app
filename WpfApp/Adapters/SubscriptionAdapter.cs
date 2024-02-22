using System.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using WpfApp.Config;
using WpfApp.Const;
using WpfApp.Models;

namespace WpfApp.Adapters;

public static class SubscriptionAdapter
{
    private static string  _selectSql = $"SELECT * FROM {DatabaseConst.TABLE_SUBSCRIPTION}";
    
    
    public static List<SubscriptionModel> LoadSubscriptions()
    {
        
        OleDbConnection myConn = new OleDbConnection(AppConfig.DATABASE_CONNECTION_STRING);  
        OleDbDataAdapter myCmd = new OleDbDataAdapter(_selectSql, myConn);  
        myConn.Open();  
        DataSet dtSet = new DataSet();  
        myCmd.Fill(dtSet, DatabaseConst.TABLE_SUBSCRIPTION);


        List<SubscriptionModel> result = new List<SubscriptionModel>();
        foreach (DataRow row in dtSet.Tables[0].Rows)
        {

            result.Add(
                new SubscriptionModel
                {
                    Id = Convert.ToInt32(row[DatabaseConst.SUBSCRIPTION_ID]),
                    GymId = Convert.ToInt32(row[DatabaseConst.GYM_ID]),
                    Description = row[DatabaseConst.SUBSCRIPTION_DESCRIPTION].ToString(),
                    Price = (decimal) row[DatabaseConst.SUBSCRIPTION_PRICE],
                });
        }

        return result;
    }
    
}