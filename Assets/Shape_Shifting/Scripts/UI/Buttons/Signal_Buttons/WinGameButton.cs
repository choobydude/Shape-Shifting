namespace ShapeShifting
{
    public class WinGameButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<WinGameCommandSignal>();
        }
    }
}
