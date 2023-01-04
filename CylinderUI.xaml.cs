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
        Cylinder thisCylinder;


        public CylinderUI(object sender)
        {
            InitializeComponent();

            thisCylinder = (Cylinder)sender;

            // Subscribe to the event
            thisCylinder.StateChanged += ThisCylinder_StateChanged;

            txtBoxName.Text = thisCylinder.Name;
            txtBoxActuatorType.Text = thisCylinder.ActuatorType;
            txtBoxID.Text = thisCylinder.ID;

            txtBoxStateBasePosition.Text = thisCylinder.getRunningStateToBasePosition();
            txtBoxStateWorkPosition.Text = thisCylinder.getRunningStateToWorkPosition();
        }

        private void ThisCylinder_StateChanged(object sender, CylinderStateEventArgs e)
        {
            txtBoxStateBasePosition.Text = e.CylinderState;
            txtBoxStateWorkPosition.Text = e.CylinderState;
        }

        private void btnControlToWork_Click(object sender, RoutedEventArgs e)
        {
            thisCylinder.ControlToWorkPosition();
        }

        private void btnControlToBase_Click(object sender, RoutedEventArgs e)
        {
            thisCylinder.ControlToBasePosition();
        }
    }
}
