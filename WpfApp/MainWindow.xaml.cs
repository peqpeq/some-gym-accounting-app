using System.ComponentModel;
using System.Data;
using System.Windows;
using WpfApp.Adapters;
using WpfApp.Models;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BindingList<TodoModel> _todoData; 
     
     public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        todoDataList.ItemsSource = TodoAdapter.LoadTodo();
        couchesDataList.ItemsSource = CouchAdapter.LoadCouches();
        clientsDataList.ItemsSource = ClientAdapter.LoadClients();
        gymsDataList.ItemsSource = GymAdapter.LoadGyms();
        accountingDataList.ItemsSource = AccountingAdapter.LoadAccountings();
        subscriptionsDataList.ItemsSource = SubscriptionAdapter.LoadSubscriptions();
    }
    

}