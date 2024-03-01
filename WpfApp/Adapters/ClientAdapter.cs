using System.Data;
using System.Data.OleDb;
using WpfApp.Const;
using WpfApp.Models;

namespace WpfApp.Adapters;

public static class ClientAdapter
{
    
    
    
    // Загрузка записей из базы данных
    public static List<ClientModel> LoadClients(ClientModel clientModelSearch)
    {

        string conditions = getCondition(clientModelSearch);
        
        // Подключение к базе данных
        OleDbConnection myConn = new OleDbConnection(DatabaseConst.DATABASE_CONNECTION_STRING);
        Console.WriteLine($"""
                            SELECT * FROM [{DatabaseConst.TABLE_CLIENTS}] 
                            {conditions} ;
                            """);
                         
        // Команда что бы достать данные
        string query = $"""
                        SELECT * FROM [{DatabaseConst.TABLE_CLIENTS}] 
                        {conditions} ;
                        """;

        
        OleDbDataAdapter myCmd = new OleDbDataAdapter(query, myConn);  
        
        myConn.Open();  
        
        DataSet dtSet = new DataSet();  
        
        myCmd.Fill(dtSet, DatabaseConst.TABLE_CLIENTS);
        
        // Создаем объекты из базы данных
        List<ClientModel> result = new List<ClientModel>();
        
        foreach (DataRow row in dtSet.Tables[0].Rows)
        {

            result.Add(
                // Создаем обьекты и добавляем их в список
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

    public static String getCondition(ClientModel clientModel)
    {
        List<string> conditions = new List<string>();


        if (clientModel.Name != null && clientModel.Name != ""  )
        {
            conditions.Add($"[{DatabaseConst.CLIENT_NAME}] like '%{clientModel.Name}%'");
        }
        if (clientModel.Forename != null && clientModel.Forename != "")
        {
            conditions.Add($"[{DatabaseConst.CLIENT_FORENAME}] like '%{clientModel.Forename}%'");
        }
        if (clientModel.PhoneNumber != null && clientModel.PhoneNumber != "")
        {
            conditions.Add($"[{DatabaseConst.CLIENT_PHONE}] like '%{clientModel.PhoneNumber}%'");
        }
        if (clientModel.CouchId != 0)
        {
            conditions.Add($"[{DatabaseConst.COUCH_ID}] = {clientModel.CouchId}");
        }

        if (conditions.Count == 0)
        {
            return "";
        }
        
        return "WHERE " + string.Join(" AND ", conditions);

    }
    
    public static void SaveClient(ClientJoinedModel clientModel)
    {
        // Подключение к базе данных
        OleDbConnection myConn = new OleDbConnection(DatabaseConst.DATABASE_CONNECTION_STRING);
        myConn.Open();

        // Команда что бы вставить данные
        string query = $" INSERT INTO [{DatabaseConst.TABLE_CLIENTS}] ([{DatabaseConst.CLIENT_NAME}],[{DatabaseConst.CLIENT_FORENAME}],[{DatabaseConst.CLIENT_PHONE}],[{DatabaseConst.COUCH_ID}]) VALUES (@Name, @Forename, @Phone, @CouchId);";
        
        OleDbCommand command = new OleDbCommand(query, myConn);
        
        // Назначаем переменные получеными значениями 
        command.Parameters.AddWithValue("@Name", clientModel.ClientName);
        command.Parameters.AddWithValue("@Forename", clientModel.ClientForename);
        command.Parameters.AddWithValue("@Phone", clientModel.ClientPhoneNumber);
        command.Parameters.AddWithValue("@CouchId", clientModel.CouchId);
        
        // Выполнить запрос
        command.ExecuteNonQuery();
        
    }

    public static void EditClient(ClientModel clientModel)
    {
        // Подключение к базе данных
        OleDbConnection myConn = new OleDbConnection(DatabaseConst.DATABASE_CONNECTION_STRING);
        myConn.Open();

        // Команда что бы вставить данные
        string query = $"UPDATE [{DatabaseConst.TABLE_CLIENTS}] SET [{DatabaseConst.CLIENT_NAME}] = @Name, [{DatabaseConst.CLIENT_FORENAME}] = @Forename, [{DatabaseConst.CLIENT_PHONE}] = @Phone, [{DatabaseConst.COUCH_ID}] = @CouchId WHERE [{DatabaseConst.CLIENT_ID}] = @Id;";

        OleDbCommand command = new OleDbCommand(query, myConn);
        
        // Назначаем переменные полученными значениями 
        command.Parameters.AddWithValue("@Name", clientModel.Name);
        command.Parameters.AddWithValue("@Forename", clientModel.Forename);
        command.Parameters.AddWithValue("@Phone", clientModel.PhoneNumber);
        command.Parameters.AddWithValue("@CouchId", clientModel.CouchId);
            
        // Выполнить запрос
        command.ExecuteNonQuery();
    }
    
    public static void DeleteClient(int? clientId)
    {
        // Подключение к базе данных
        OleDbConnection myConn = new OleDbConnection(DatabaseConst.DATABASE_CONNECTION_STRING);  
        // Команда что бы удалить данные
        string query = $"DELETE FROM [{DatabaseConst.TABLE_CLIENTS}] WHERE [{DatabaseConst.CLIENT_ID}] = @Id;";
        myConn.Open();
        
        OleDbCommand command = new OleDbCommand(query, myConn);
        
        // Назначаем переменные получеными значениями 
        command.Parameters.AddWithValue("@Id", clientId);
        
        // Выполнить запрос
        command.ExecuteNonQuery();
    }

}