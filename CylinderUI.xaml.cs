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
using System.Windows.Shapes;

namespace MachineControl
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CylinderUI : Window
    {
        private Cylinder activeCylinder
        {
            get; set;
        }

        public CylinderUI(object sender)
        {
            InitializeComponent();

            activeCylinder = (Cylinder)sender;

            txtBoxName.Text = activeCylinder.Name;
            txtBoxActuatorType.Text = activeCylinder.ActuatorType;
            txtBoxID.Text = activeCylinder.ID;

            txtBoxStateBasePosition.Text = activeCylinder.getRunningStateToBasePosition();
            txtBoxStateWorkPosition.Text = activeCylinder.getRunningStateToWorkPosition();
        }

        private void btnControlToWork_Click(object sender, RoutedEventArgs e)
        {
            activeCylinder.ControlToWorkPosition();
        }

        private void btnControlToBase_Click(object sender, RoutedEventArgs e)
        {
            activeCylinder.ControlToBasePosition();
        }
    }
}
