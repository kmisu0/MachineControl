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
        // Fields
        private bool _runToBasePosition;
        private bool _runToWorkPosition;
        private bool _releasedToBasePosition;
        private bool _releasedToWorkPosition;

        // Constructors
        public Cylinder(string name, string identifier)
            : base(name, identifier, "Cylinder")
        {
            _releasedToBasePosition = true;
        }

        // Methods
        public void ControlToBasePosition()
        {
            _runToBasePosition = true;
            _runToWorkPosition = false;
            OnStateChanged(getRunningState());
        }

        public void ControlToWorkPosition()
        {
            _runToBasePosition = false;
            _runToWorkPosition = true;
            OnStateChanged(getRunningState());
        }
        
        public CylinderStateEventArgs getRunningState()
        {
            CylinderStateEventArgs result = new CylinderStateEventArgs();

            if (_runToBasePosition)
            {
                if (!result.cylinderStates.Contains("runToBasePosition"))
                {
                    result.cylinderStates.Add("runToBasePosition");
                }
            }
            else
            {
                if (result.cylinderStates.Contains("runToBasePosition"))
                {
                    result.cylinderStates.Remove("runToBasePosition");
                }
            }

            if (_releasedToBasePosition)
            {
                if (!result.cylinderStates.Contains("releasedToBasePosition"))
                {
                    result.cylinderStates.Add("releasedToBasePosition");
                }
            }
            else
            {
                if (result.cylinderStates.Contains("releasedToBasePosition"))
                {
                    result.cylinderStates.Remove("releasedToBasePosition");
                }
            }

            if (_runToWorkPosition)
            {
                if (!result.cylinderStates.Contains("runToWorkPosition"))
                {
                    result.cylinderStates.Add("runToWorkPosition");
                }
            }
            else
            {
                if (!result.cylinderStates.Contains("runToWorkPosition"))
                {
                    result.cylinderStates.Remove("runToWorkPosition");
                }
            }

            if (_releasedToWorkPosition)
            {
                if (!result.cylinderStates.Contains("releasedToWorkPosition"))
                {
                    result.cylinderStates.Add("releasedToWorkPosition");
                }
            }
            else
            {
                if (result.cylinderStates.Contains("releasedToWorkPosition"))
                {
                    result.cylinderStates.Remove("releasedToWorkPosition");
                }
            }

            return result;
        }

        // Events
        public event EventHandler<CylinderStateEventArgs> StateChanged;

        protected virtual void OnStateChanged(CylinderStateEventArgs eventArgs)
        {
            if (StateChanged != null)
            {
                StateChanged(this, eventArgs);
                
            }
        }
    }

    public class CylinderStateEventArgs : EventArgs
    {
        public List<string> cylinderStates;

        public CylinderStateEventArgs()
        {
            cylinderStates = new List<string>();
        }
    }
}
