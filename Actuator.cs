using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public void ControlToBasePosition()
        {
            _runToBasePosition = true;
            _runToWorkPosition = false;
            OnStateChanged(getRunningStateToBasePosition());
        }

        public void ControlToWorkPosition()
        {
            _runToBasePosition = false;
            _runToWorkPosition = true;
            OnStateChanged(getRunningStateToBasePosition());
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

        public event EventHandler<CylinderStateEventArgs> StateChanged;

        protected virtual void OnStateChanged(string BasePos)
        {
            if (StateChanged != null)
            {
                StateChanged(this, new CylinderStateEventArgs() { CylinderState = BasePos });
            }
        }
    }

    public class CylinderStateEventArgs : EventArgs
    {
        public string CylinderState { get; set; }
    }
}
