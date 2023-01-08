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
        }

        private void ThisCylinder_StateChanged(object sender, CylinderStateEventArgs e)
        {
            foreach (string state in e.cylinderStates)
            {
                if (state.Contains("runToBasePosition"))
                {
                    txtBoxStateBasePosition.Text = "Run To BP";
                    txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0xFF ,0x00,0xFF,0x00));
                    break;
                }
                else if (state.Contains("releasedToBasePos"))
                {
                    txtBoxStateBasePosition.Text = "Released To BP";
                    txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0xFF ,0xAA, 0xFF, 0xAA));
                    break;
                }
                else
                {
                    txtBoxStateBasePosition.Text = "No Operation";
                    txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
                }
            }

            foreach (string state in e.cylinderStates)
            {
                if (state.Contains("runToWorkPosition"))
                {
                    txtBoxStateWorkPosition.Text = "Run To WP";
                    txtBoxStateWorkPosition.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xFF, 0x00));
                    break;
                }
                else if (state.Contains("releasedToWorkPosition"))
                {
                    txtBoxStateWorkPosition.Text = "Released To WP";
                    txtBoxStateWorkPosition.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xAA, 0xFF, 0xAA));
                    break;
                }
                else
                {
                    txtBoxStateWorkPosition.Text = "No Operation";
                    txtBoxStateWorkPosition.Background = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
                }
            }
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
