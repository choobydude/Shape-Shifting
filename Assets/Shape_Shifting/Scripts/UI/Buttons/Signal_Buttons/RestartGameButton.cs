namespace ShapeShifting
{
    public class RestartGameButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<RestartGameCommandSignal>();
        }
    }
}

