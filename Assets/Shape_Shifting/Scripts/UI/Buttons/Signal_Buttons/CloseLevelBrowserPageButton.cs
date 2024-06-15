namespace ShapeShifting
{
    public class CloseLevelBrowserPageButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<CloseLevelBrowserPageCommandSignal>();
        }
    }
}

