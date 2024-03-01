using System.Windows;
using WpfApp.Adapters;
using WpfApp.Models;

namespace WpfApp.Windows;

public partial class CreateEditClientWindow : Window
{
    
    // Список тренеров
    public List<CouchModel> CouchList { get; set; }
    private ClientJoinedModel ClientModel;
    private Action save;
    
    public CreateEditClientWindow(ClientJoinedModel createEditModel, Action saveAction)
    {
        InitializeComponent();
        ClientModel = createEditModel;
        
        this.DataContext = ClientModel;
        save = saveAction;
        
        // Загружаем тренеров 
        CouchList = CouchAdapter.LoadCouches();
        // Устанавливаем значение для выбора тренеров
        CouchSelect.ItemsSource = CouchList;

        // Если у клиента есть тренер
        if (createEditModel.CouchId != null)
        {
            // Найти тренера обслуживающего клиента
            foreach (var couch in CouchList) {
                // Если код тренера такой же как "код тренера" у клиента, выбираем тренера 
                if (couch.Id == createEditModel.CouchId )
                {
                    CouchSelect.SelectedItem = couch;

                }

            }
        
        }

    }
    
    private void SaveButton_Clicked(object sender, RoutedEventArgs e)
    {
        // Получаем значение выбранного тренера из combobox
        CouchModel selectedCouch = (CouchModel)CouchSelect.SelectedItem;
        
        // Ecли тренер выбран - назначем его для клиента
        if (selectedCouch != null)
        {
            // Назначаем значения для клиента
            ClientModel.CouchId= selectedCouch.Id;
            ClientModel.CouchName= selectedCouch.Name;
            ClientModel.CouchPhoneNumber= selectedCouch.PhoneNumber;
        }
        // Используем полученный от вызывающего окна функцию сохранения
        save();
        Close();
    }
}