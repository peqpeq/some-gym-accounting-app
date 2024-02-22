using System.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using WpfApp.Config;
using WpfApp.Const;
using WpfApp.Models;

namespace WpfApp.Adapters;

public static class ClientAdapter
{
    private static string  _selectSql = $"SELECT * FROM {DatabaseConst.TABLE_CLIENTS}";
    
    
    public static List<ClientModel> LoadClients()
    {
        
        OleDbConnection myConn = new OleDbConnection(AppConfig.DATABASE_CONNECTION_STRING);  
        OleDbDataAdapter myCmd = new OleDbDataAdapter(_selectSql, myConn);  
        myConn.Open();  
        DataSet dtSet = new DataSet();  
        myCmd.Fill(dtSet, DatabaseConst.TABLE_CLIENTS);


        List<ClientModel> result = new List<ClientModel>();
        foreach (DataRow row in dtSet.Tables[0].Rows)
        {

            result.Add(
                new ClientModel
                {
                    Id = Convert.ToInt32(row[DatabaseConst.CLIENT_ID]),
                    Name = row[DatabaseConst.CLIENT_NAME].ToString(),
                    Forename = row[DatabaseConst.CLIENT_FORENAME].ToString(),
                    PhoneNumber = row[DatabaseConst.CLIENT_PHONE].ToString(),
                    CouchId =  Convert.ToInt32(row[DatabaseConst.COUCH_ID].ToString()),
                });
        }

        return result;
    }
    
}