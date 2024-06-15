namespace ShapeShifting
{
    public class LoseGameButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<LoseGameCommandSignal>();
        }
    }
}

