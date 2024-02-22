using System.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using WpfApp.Config;
using WpfApp.Const;
using WpfApp.Models;

namespace WpfApp.Adapters;

public static class GymAdapter
{
    private static string  _selectSql = $"SELECT * FROM {DatabaseConst.TABLE_GYM}";
    
    
    public static List<GymModel> LoadGyms()
    {
        
        OleDbConnection myConn = new OleDbConnection(AppConfig.DATABASE_CONNECTION_STRING);  
        OleDbDataAdapter myCmd = new OleDbDataAdapter(_selectSql, myConn);  
        myConn.Open();  
        DataSet dtSet = new DataSet();  
        myCmd.Fill(dtSet, DatabaseConst.TABLE_GYM);


        List<GymModel> result = new List<GymModel>();
        foreach (DataRow row in dtSet.Tables[0].Rows)
        {

            result.Add(
                new GymModel
                {
                    Id = Convert.ToInt32(row[DatabaseConst.GYM_ID]),
                    Name = row[DatabaseConst.GYM_NAME].ToString(),
                });
        }

        return result;
    }
    
}