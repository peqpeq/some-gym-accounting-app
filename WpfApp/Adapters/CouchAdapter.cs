using System.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using WpfApp.Const;
using WpfApp.Models;

namespace WpfApp.Adapters;

public static class CouchAdapter
{
    private static string  _selectSql = $"SELECT * FROM {DatabaseConst.TABLE_COUCH} ";
    
    
    public static List<CouchModel> LoadCouches()
    {
        // Подключение к базе данных
        OleDbConnection myConn = new OleDbConnection(DatabaseConst.DATABASE_CONNECTION_STRING);  
        OleDbDataAdapter myCmd = new OleDbDataAdapter(_selectSql, myConn);  
        
        
        myConn.Open();  
        
        // Создаем объекты из базы данных
        DataSet dtSet = new DataSet();  
        myCmd.Fill(dtSet, DatabaseConst.TABLE_COUCH);


        List<CouchModel> result = new List<CouchModel>();
        foreach (DataRow row in dtSet.Tables[0].Rows)
        {

            result.Add(
                // Создаем обьекты и добавляем их в список
                new CouchModel
                {
                    Id = Convert.ToInt32(row[DatabaseConst.COUCH_ID]),
                    Name = row[DatabaseConst.COUCH_NAME].ToString(),
                    Salary = (decimal) row[DatabaseConst.COUCH_SALARY],
                    PhoneNumber = row[DatabaseConst.COUCH_PHONE].ToString(),
                });
        }

        return result;
    }
    
}