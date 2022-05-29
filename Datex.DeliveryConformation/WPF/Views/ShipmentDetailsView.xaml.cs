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

namespace Datex.DeliveryConformation.WPF.Views
{
    /// <summary>
    /// Interaction logic for ShipmentDetailsView.xaml
    /// </summary>
    public partial class ShipmentDetailsView : UserControl
    {
        public ShipmentDetailsView()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Visibility = e.NewValue == null ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
