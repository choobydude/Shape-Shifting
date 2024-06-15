using Zenject;

namespace WhackAMole
{
    public class PlayButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<StartGameCommandSignal>();
        }
    }
}

