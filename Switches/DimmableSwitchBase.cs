using System.Collections.Generic;

namespace Switches
{
    public abstract class DimmableSwitchBase : LightswitchBase
    {
        private HashSet<IDimmable> _dimmables;

        protected int CurrentPosition { get; set; }

        protected HashSet<IDimmable> Dimmables
        {
            get { return _dimmables ?? (_dimmables = new HashSet<IDimmable>()); }
        }

        public void AddDimmable(IDimmable dimmable)
        {
            Dimmables.Add(dimmable);
        }

        public abstract void Lower(int distance);

        public abstract void Raise(int distance);
    }
}