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

namespace MachineControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            CylinderUI uiCylinder = new CylinderUI(App.Cylinder1);
            uiCylinder.Show();
        }

        private void Open2_Click(object sender, RoutedEventArgs e)
        {
            CylinderUI uiCylinder2 = new CylinderUI(App.Cylinder2);
            uiCylinder2.Show();
        }
    }
}
