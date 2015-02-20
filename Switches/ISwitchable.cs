namespace Switches
{
    public enum SwitchState
    {
        Off,
        On
    }

    public interface ISwitchable
    {
        SwitchState State { get; }

        void Switch();
    }
}