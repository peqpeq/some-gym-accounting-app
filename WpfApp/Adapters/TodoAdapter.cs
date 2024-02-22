using System.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using WpfApp.Config;
using WpfApp.Models;

namespace WpfApp.Adapters;

public static class TodoAdapter
{
    private static string  _selectSql = "SELECT * FROM Todo";
    public static List<TodoModel> LoadTodo()
    {
        
        OleDbConnection myConn = new OleDbConnection(AppConfig.DATABASE_CONNECTION_STRING);  
        OleDbDataAdapter myCmd = new OleDbDataAdapter(_selectSql, myConn);  
        myConn.Open();  
        DataSet dtSet = new DataSet();  
        myCmd.Fill(dtSet, "Todo");


        List<TodoModel> result = new List<TodoModel>();
        foreach (DataRow row in dtSet.Tables[0].Rows)
        {

            result.Add(
                new TodoModel
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Todo = row["Todo"].ToString(),
                    Date = Convert.ToDateTime(row["Date"])
                });
        }

        return result;
    }
    
}