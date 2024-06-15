using Zenject;

namespace ShapeShifting
{
    public abstract class SignalButtonBase : Button
    {
        [Inject]
        protected SignalBus SignalBus;
    }
}

