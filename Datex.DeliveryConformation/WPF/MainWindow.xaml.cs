using API = Datex.DeliveryConformation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Datex.DeliveryConformation.Shared.Interfaces.Models;

namespace Datex.DeliveryConformation.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private API.DeliveryConformationAPI api;

        public MainWindow()
        {
            api = new API.DeliveryConformationAPI("http://localhost:5011/", new System.Net.Http.HttpClient());

            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            IDeliveryTruck t = await api.DeliveryTrucksAsync();
            ;
        }
    }
}
