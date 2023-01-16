using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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

        private bool _blinkBp;

        public CylinderUI(object sender)
        {
            InitializeComponent();

            // Subscribe to Timer
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();

            thisCylinder = (Cylinder)sender;
            visuCylinderState(thisCylinder.getRunningState());
            txtBoxName.Text = thisCylinder.Name;
            txtBoxActuatorType.Text = thisCylinder.ActuatorType;
            txtBoxID.Text = thisCylinder.ID;

            // Subscribe to the event
            thisCylinder.StateChanged += ThisCylinder_StateChanged;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (txtBoxStateBasePosition.Text == "Run To BP")
            {
                if (_blinkBp)
                {
                    txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xFF, 0x00));
                }
                else
                {
                    txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                }
            }
             _blinkBp = !_blinkBp;
        }

        private void ThisCylinder_StateChanged(object sender, CylinderStateEventArgs e)
        {
            visuCylinderState(e);
        }

        private void visuCylinderState(CylinderStateEventArgs cylinderState)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                // Base Position
                if (cylinderState.cylinderBpStates.Count == 0)
                {
                    txtBoxStateBasePosition.Text = "No Operation";
                    txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
                }
                else
                {
                    foreach (string state in cylinderState.cylinderBpStates)
                    {
                        if (state.Contains("inBasePosition"))
                        {
                            txtBoxStateBasePosition.Text = "In BP";
                            txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xFF, 0x00));
                            break;
                        }
                        else if (state.Contains("runToBasePosition"))
                        {
                            txtBoxStateBasePosition.Text = "Run To BP";
                            txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xFF, 0x00));
                            break;
                        }
                        else if (state.Contains("releasedToBasePos"))
                        {
                            txtBoxStateBasePosition.Text = "Released To BP";
                            txtBoxStateBasePosition.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xAA, 0xFF, 0xAA));
                            break;
                        }
                    }
                }

                // Work Position
                if (cylinderState.cylinderWpStates.Count == 0)
                {
                    txtBoxStateWorkPosition.Text = "No Operation";
                    txtBoxStateWorkPosition.Background = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
                }
                else
                {
                    foreach (string state in cylinderState.cylinderWpStates)
                    {
                        if (state.Contains("inWorkPosition"))
                        {
                            txtBoxStateWorkPosition.Text = "In WP";
                            txtBoxStateWorkPosition.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xFF, 0x00));
                            break;
                        }
                        else if (state.Contains("runToWorkPosition"))
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
                    }
                }
            }));
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
