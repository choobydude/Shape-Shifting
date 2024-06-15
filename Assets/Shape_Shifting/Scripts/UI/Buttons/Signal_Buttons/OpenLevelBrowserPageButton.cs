namespace WhackAMole
{
    public class OpenLevelBrowserPageButton : SignalButtonBase
    {

        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<OpenLevelBrowserPageCommandSignal>();
        }
    }
}