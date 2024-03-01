using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp.Adapters;
using WpfApp.Models;
using WpfApp.Windows;

namespace WpfApp;

public partial class ClientWindow : Window
{

    // Объект клиента для изменения и создания
    public ClientJoinedModel CreateEditClientModel { get; set; }
    public List<CouchModel> CouchList { get; set; }

    
    public ClientWindow()
    {
        InitializeComponent();

        
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

        // Обновляем значения интерфейса
        
        // Загружаем тренеров 
        CouchList = new List<CouchModel>
        {
            // Добавить возможность поиска без тренера для поиска
            {
                new CouchModel() { Id = 0, Name = "" }
            }
        };
        CouchList.AddRange(CouchAdapter.LoadCouches());
        
        // Устанавливаем значение для выбора тренеров
        CouchSelect.ItemsSource = CouchList;
        // Выбрать вариант без тренера поулномочию 
        CouchSelect.SelectedItem = CouchList.First();

        
        _refresh();
    }


    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        // Обработка нажатия на кнопку "Изменить"
        Button button = sender as Button;
        CreateEditClientModel = button.DataContext as ClientJoinedModel; // Получение объекта данных
        
        
        // Создание нового окна
        CreateEditClientWindow editWindow = new CreateEditClientWindow(
            CreateEditClientModel,
            // Передаем окну действие которое нужно совершить при сохранении
            () => _saveClient());
        
        // Назначаем основное окно владельцем вызванного окна для того что бы вызвать его по центру
        editWindow.Owner = this; 
        editWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        
        // Показать новое окно с помощью метода Show() 
        editWindow.Show();

    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        // Обработка нажатия на кнопку "Удалить"
        Button button = sender as Button;
        ClientJoinedModel item = button.DataContext as ClientJoinedModel; // Получение объекта данных, связанного с текущей строкой
        
        // Удалить клиента к которму принадлежит переданный "Код клиента"
        _deleteClient(item.ClientId);
        
        // Обновить интерфейс
        _refresh();
    }

    private void ButtonAddNew_OnClick(object sender, RoutedEventArgs e)
    {

        CreateEditClientModel = new ClientJoinedModel();
        
        // Создание нового экземпляра окна
        CreateEditClientWindow otherWindow = new CreateEditClientWindow(
            CreateEditClientModel, 
            // Передаем окну действие которое нужно совершить при сохранении
            () => _saveClient()
            );
                
        // Назначаем основное окно владельцем вызванного окна для того что бы вызвать его по центру
        otherWindow.Owner = this; 
        otherWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        
        // Показать новое окно с помощью метода Show() (для немодального окна)
        otherWindow.Show();

    }
    private List<ClientJoinedModel> _loadJoinedClients()
    {
        var couches = CouchAdapter.LoadCouches();
        
        // Сделать объект для поиска

        var search = new ClientModel()
        {
            Name = searchName.Text,
            Forename = searchForename.Text,
            PhoneNumber = searchPhone.Text,
            CouchId = ((CouchModel)CouchSelect.SelectedItem).Id
        };
        
        // Загрузить клиентов из базы данных
        List<ClientModel> clientModels = ClientAdapter.LoadClients(search);
        
        // Организовать тренеров по их коду в словарь, для того что бы соединить с клиентами. Словарь - [ключ - Код тренера, значение - Тренер]
        Dictionary<int, CouchModel> couchesDict = couches.ToDictionary(couch => couch.Id);

        List<ClientJoinedModel> result = new List<ClientJoinedModel>();
        
        // Меняем обьект и  добавляем каждому клиенту тренера при помощи словаря 
        foreach (var client in clientModels)
        {
            result.Add(
                new ClientJoinedModel()
                {
                    // Назначаем поля клиента
                    ClientId = client.Id,
                    ClientName = client.Name,
                    ClientForename = client.Forename,
                    ClientPhoneNumber = client.PhoneNumber,
                    CouchId = couchesDict[client.CouchId].Id, 
                    // Назначаем поля тренера через словарь используя значение "Код тренера" у клиента
                    CouchName = couchesDict[client.CouchId].Name,
                    CouchSalary = couchesDict[client.CouchId].Salary,
                    CouchPhoneNumber = couchesDict[client.CouchId].PhoneNumber,
                });
        }
        // Собранный список назначаем значением для таблицы в интерфейсе 
        return result;
    }
    
    
    private void _refresh()
    {
        // Обнуляем значение источника для таблицы
        clientJoinedModelList.ItemsSource = null;
        
        // Назначаем значение списка клиента источником для таблицы 
        clientJoinedModelList.ItemsSource = _loadJoinedClients();
        clientJoinedModelList.Items.Refresh();
    }

    private void _deleteClient(int id)
    {
        // Удаляем из списка клиентов всех клиентов где код клиента равен удаляемому элементу
        ClientAdapter.DeleteClient(id);
        
        // Обновляем интерфейс
        _refresh();
    }

    private async void _saveClient()
    {
        // Если у переданного клиента есть "Код клиента" - значит запись уже существует, по-этому мы удаляем старую запись
        if (CreateEditClientModel.ClientId != 0)
        {
            ClientAdapter.DeleteClient(CreateEditClientModel.ClientId);
        }
        // Добавляем новую запись клиента в бьазу данных
        ClientAdapter.SaveClient(CreateEditClientModel);
        
        // Не успевает, поэтому ждем пол секундочки
        await Task.Delay(500);
        _refresh();
        
        
    }

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        _refresh();

    }

    private void ClearSearch_OnClick(object sender, RoutedEventArgs e)
    {
        // очищаем поиск
        CouchSelect.SelectedItem = CouchList.First();
        searchName.Text = "";
        searchForename.Text = "";
        searchPhone.Text = "";
        
        _refresh();
    }

}