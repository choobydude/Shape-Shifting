namespace ShapeShifting
{
    public class NextLevelButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<LoadNextLevelCommandSignal>();
        }
    }
}

