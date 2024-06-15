namespace WhackAMole
{
    public class RVPage : PageBase
    {
        #region Signal Handling
        protected override void subscribeSignals()
        {
            SignalBus.Subscribe<OpenRVPageCommandSignal>(Show);
            SignalBus.Subscribe<CloseRVPageCommandSignal>(Hide);
            SignalBus.Subscribe<GameStartedSignal>(Hide);
        }

        protected override void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<OpenRVPageCommandSignal>(Show);
            SignalBus.TryUnsubscribe<CloseRVPageCommandSignal>(Hide);
            SignalBus.TryUnsubscribe<GameStartedSignal>(Hide);
        }
        #endregion
    }
}

