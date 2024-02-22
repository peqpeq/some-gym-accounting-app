using System.Runtime.InteropServices.JavaScript;

namespace WpfApp.Const;

public static class DatabaseConst
{
    public static String TABLE_COUCH = "Тренеры";
    public static String COUCH_ID = "Код тренера";
    public static String COUCH_NAME = "ФИО";
    public static String COUCH_SALARY = "Оклад";
    public static String COUCH_PHONE = "Телефон";
    
    public static String TABLE_CLIENTS = "Клиенты";
    public static String CLIENT_ID = "Код клиента";
    public static String CLIENT_NAME = "Имя";
    public static String CLIENT_FORENAME = "Фамилия";
    public static String CLIENT_PHONE = "Номер Телефона";
    
    public static String TABLE_GYM = "Залы";
    public static String GYM_ID = "Код зала";
    public static String GYM_NAME = "Наименование";
    
    public static String TABLE_SUBSCRIPTION = "Абонементы";
    public static String SUBSCRIPTION_ID = "Код абонемента";
    public static String SUBSCRIPTION_DESCRIPTION = "Описание";
    public static String SUBSCRIPTION_PRICE = "Цена";

    public static String TABLE_ACCOUNTING = "Учет";
    public static String ACCOUNTING_ID = "Код учета";
    public static String ACCOUNTING_MOUNTH = "Месяц";
}