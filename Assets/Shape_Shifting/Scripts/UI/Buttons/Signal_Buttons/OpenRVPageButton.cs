namespace WhackAMole
{
    public class OpenRVPageButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<OpenRVPageCommandSignal>();
        }
    }
}
