using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        // Constructors
        public Cylinder(string name, string identifier)
            : base(name, identifier, "Cylinder")
        {
            _sensorBasePositionPrevious = _sensorBasePosition;
            _sensorWorkPositionPrevious = _sensorWorkPosition;
        }

        // Fields
        private bool _sensorBasePosition;
        private bool _sensorWorkPosition;
        private bool _sensorBasePositionPrevious;
        private bool _sensorWorkPositionPrevious;
        private bool _runToBasePosition;
        private bool _runToWorkPosition;
        private bool _releasedToBasePosition;
        private bool _releasedToWorkPosition;

        // Properties
        public bool SensorBasePosition
        {
            get { return _sensorBasePosition; }
            set
            {
                _sensorBasePosition = value;
                if (SensorBasePosition != _sensorBasePositionPrevious)
                {
                    OnStateChanged(getRunningState());
                }
                _sensorBasePositionPrevious = value;
            }
        }
        public bool SensorWorkPosition
        { 
            get { return _sensorWorkPosition; }
            set
            {
                _sensorWorkPosition = value;
                if (SensorWorkPosition != _sensorWorkPositionPrevious)
                {
                    OnStateChanged(getRunningState());
                }
                _sensorWorkPositionPrevious = value;
            }
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
        public void ReleaseToBasePosition()
        {
            _releasedToBasePosition = true;
        }
        public void ReleaseToWorkPosition()
        {
            _releasedToWorkPosition = true;
        }

        protected virtual void OnStateChanged(CylinderStateEventArgs eventArgs)
        {
            if (StateChanged != null)
            {
                StateChanged(this, eventArgs);
            }
        }

        public CylinderStateEventArgs getRunningState()
        {
            CylinderStateEventArgs result = new CylinderStateEventArgs();

            if (_sensorBasePosition)
            {
                if (!result.cylinderBpStates.Contains("inBasePosition"))
                {
                    result.cylinderBpStates.Add("inBasePosition");
                    Trace.WriteLine("inBasePosition is added to list.");
                    Trace.WriteLine("_sensorBase Position is in " + _sensorBasePosition.ToString());
                }
            }

            if (_runToBasePosition)
            {
                if (!result.cylinderBpStates.Contains("runToBasePosition"))
                {
                    result.cylinderBpStates.Add("runToBasePosition");
                }
            }

            if (_releasedToBasePosition)
            {
                if (!result.cylinderBpStates.Contains("releasedToBasePosition"))
                {
                    result.cylinderBpStates.Add("releasedToBasePosition");
                }
            }

            if (_sensorWorkPosition)
            {
                if (!result.cylinderWpStates.Contains("inWorkPosition"))
                {
                    result.cylinderWpStates.Add("inWorkPosition");
                }
            }

            if (_runToWorkPosition)
            {
                if (!result.cylinderWpStates.Contains("runToWorkPosition"))
                {
                    result.cylinderWpStates.Add("runToWorkPosition");
                }
            }

            if (_releasedToWorkPosition)
            {
                if (!result.cylinderWpStates.Contains("releasedToWorkPosition"))
                {
                    result.cylinderWpStates.Add("releasedToWorkPosition");
                }
            }

            return result;
        }

        // Events
        public event EventHandler<CylinderStateEventArgs> StateChanged;
    }

    public class CylinderStateEventArgs : EventArgs
    {
        public List<string> cylinderBpStates;
        public List<string> cylinderWpStates;

        public CylinderStateEventArgs()
        {
            cylinderBpStates = new List<string>();
            cylinderWpStates = new List<string>();
        }
    }
}
