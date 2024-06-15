using Zenject;

namespace ShapeShifting
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

