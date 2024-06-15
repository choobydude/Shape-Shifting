using Zenject;

namespace WhackAMole
{
    public class WinPage : PageBase
    {
        #region Signal Handling
        protected override void subscribeSignals()
        {
            SignalBus.Subscribe<GameWonSignal>(Show);
            SignalBus.Subscribe<GameUnloadedSignal>(Hide);
        }

        protected override void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<GameWonSignal>(Show);
            SignalBus.TryUnsubscribe<GameUnloadedSignal>(Hide);
        }
        #endregion
    }
}

