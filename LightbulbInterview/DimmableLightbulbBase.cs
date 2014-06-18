using Switches;

namespace LightbulbInterview
{
    public abstract class DimmableLightbulbBase : LightbulbBase, IDimmable
    {
        protected int Intensity { get; private set; }

        public void SetOutput(int newOutput)
        {
            Intensity = newOutput;
        }

        protected DimmableLightbulbBase(int lumens, int wattage) : base(lumens, wattage)
        {
            Intensity = 0;
        }
    }
}