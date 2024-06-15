namespace WhackAMole
{
    public class CloseRVPageButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<CloseRVPageCommandSignal>();
        }
    }
}