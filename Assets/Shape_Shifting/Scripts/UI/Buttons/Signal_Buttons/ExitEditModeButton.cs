namespace ShapeShifting
{
    public class ExitEditModeButton : SignalButtonBase
    {
        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire<ExitShapeEditorCommandSignal>();
        }
    }
}