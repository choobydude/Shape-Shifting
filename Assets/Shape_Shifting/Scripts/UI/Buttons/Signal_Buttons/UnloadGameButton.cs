using Zenject;

namespace ShapeShifting
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

