using System;
using Switches;

namespace LightbulbInterview
{
    public abstract class LightbulbBase : ISwitchable
    {
        protected SwitchState SwitchState = SwitchState.Off;

        public int Lumens { get; private set; }

        public int Wattage { get; private set; }

        public virtual int Light
        {
            get { return Lumens; }
        }

        /// <summary>
        /// Calculates the amount of energy used in kWh.
        /// </summary>
        /// <param name="timeOn">Amount of time the lightbulb has been on.</param>
        /// <returns>Amount of energy used in kWh.</returns>
        /// <remarks>kWh is calculated as the wattage * timeOn (in hours) / 1000</remarks>
        public virtual double EnergyUsed(TimeSpan timeOn)
        {
            return Wattage*timeOn.TotalHours;
        }

        public SwitchState State
        {
            get { return SwitchState; }
        }

        public virtual void Switch()
        {
        }

        protected LightbulbBase(int lumens, int wattage)
        {
        }
    }
}
