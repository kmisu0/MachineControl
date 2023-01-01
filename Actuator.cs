using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MachineControl
{
    public abstract class Actuator
    {
        public Actuator(string name, string identifier)
        {
            Name = name;
            ID = identifier;
        }

        protected Actuator(string name, string identifier, string actuatorType) : this(name, identifier)
        {
            ActuatorType = actuatorType;
        }

        public string Name
        {
            get;
        }

        public string ID
        {
            get;
        }

        public string ActuatorType
        { 
            get;
        }
    }

    public partial class Cylinder : Actuator
    {
        private bool _runToBasePosition;
        private bool _runToWorkPosition;

        public Cylinder(string name, string identifier)
            : base(name, identifier, "Cylinder")
        {
        }   

        public bool ControlToBasePosition()
        {
            _runToBasePosition = true;
            _runToWorkPosition = false;
            return true;
        }

        public bool ControlToWorkPosition()
        {
            _runToBasePosition = false;
            _runToWorkPosition = true;
            return true;
        }

        public string getRunningStateToBasePosition()
        {
            if (_runToBasePosition)
            {
                return "Run to BP";
            }
            return "No Operation";
        }
        public string getRunningStateToWorkPosition()
        {
            if (_runToWorkPosition)
            {
                return "Run To WP";
            }
            return "No Operation";

        }
    }
}
