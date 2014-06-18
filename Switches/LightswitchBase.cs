using System.Collections.Generic;

namespace Switches
{
    public abstract class LightswitchBase
    {
        private HashSet<ISwitchable> _switchables;
        
        protected HashSet<ISwitchable> Switchables
        {
            get { return _switchables ?? (_switchables = new HashSet<ISwitchable>()); }
        }

        public void AddSwitchable(ISwitchable switchable)
        {
            Switchables.Add(switchable);
        }

        public abstract void Toggle();
    }
}
