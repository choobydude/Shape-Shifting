namespace ShapeShifting
{
    public class LosePage : PageBase
    {
        #region Signal Handling
        protected override void subscribeSignals()
        {
            SignalBus.Subscribe<GameLostSignal>(Show);
            SignalBus.Subscribe<GameUnloadedSignal>(Hide);
        }

        protected override void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<GameLostSignal>(Show);
            SignalBus.TryUnsubscribe<GameUnloadedSignal>(Hide);
        }
        #endregion
    }
}

