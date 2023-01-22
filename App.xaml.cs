using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace MachineControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Cylinder Cylinder1 = new Cylinder("horizontalCylinder", "130Z1");
        public static Cylinder Cylinder2 = new Cylinder("verticalzylinder", "230Z1");

        [STAThread]
        public static void Main(string[] args)
        {
            //Task task1 = Task.Factory.StartNew(() => startSimulator());

            var application = new App();
            application.InitializeComponent();
            application.Run();

        }

        private static void startSimulator()
        {
            ConsoleKeyInfo pushedKey;
            bool exitLoop = false;
            while (!exitLoop)
            {
                Console.Clear();
                Console.WriteLine("*** Welcome To Simulator of the Machine Control ***");
                Console.WriteLine("The actually state of positionsensors is:");
                Console.WriteLine("Cylinder 1 Base Position is: " + Cylinder1.SensorBasePosition.ToString());
                Console.WriteLine("Cylinder 1 Work Position is: " + Cylinder1.SensorWorkPosition.ToString());
                Console.WriteLine("Cylinder 2 Base Position is: " + Cylinder2.SensorBasePosition.ToString());
                Console.WriteLine("Cylinder 2 Work Position is: " + Cylinder2.SensorWorkPosition.ToString());
                Console.WriteLine();
                Console.WriteLine("To toolge cylinder 1 BP sensor please push: CTRL+1");
                Console.WriteLine("To toolge cylinder 1 WP sensor please push: CTRL+2");
                Console.WriteLine("To toolge cylinder 2 BP sensor please push: CTRL+3");
                Console.WriteLine("To toolge cylinder 2 WP sensor please push: CTRL+4");
                Console.WriteLine("To close the simulator push: CTRL+e");
                pushedKey = Console.ReadKey();

                Cylinder1.SensorBasePosition = (pushedKey.Modifiers == ConsoleModifiers.Control && pushedKey.Key == ConsoleKey.D1) ? !Cylinder1.SensorBasePosition : Cylinder1.SensorBasePosition;
                Cylinder1.SensorWorkPosition = (pushedKey.Modifiers == ConsoleModifiers.Control && pushedKey.Key == ConsoleKey.D2) ? !Cylinder1.SensorWorkPosition : Cylinder1.SensorWorkPosition;
                Cylinder2.SensorBasePosition = (pushedKey.Modifiers == ConsoleModifiers.Control && pushedKey.Key == ConsoleKey.D3) ? !Cylinder2.SensorBasePosition : Cylinder2.SensorBasePosition;
                Cylinder2.SensorWorkPosition = (pushedKey.Modifiers == ConsoleModifiers.Control && pushedKey.Key == ConsoleKey.D4) ? !Cylinder2.SensorWorkPosition : Cylinder2.SensorWorkPosition;

                exitLoop = (pushedKey.Modifiers == ConsoleModifiers.Control && pushedKey.Key == ConsoleKey.E);
            }        
        }
    }
}
