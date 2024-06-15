using Zenject;

namespace WhackAMole
{
    public abstract class SignalButtonBase : Button
    {
        [Inject]
        protected SignalBus SignalBus;
    }
}

