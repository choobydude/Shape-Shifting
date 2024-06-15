using Zenject;

namespace WhackAMole
{
    public class UnloadGameButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<UnloadGameCommandSignal>();
        }
    }
}

