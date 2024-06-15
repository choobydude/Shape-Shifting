using Zenject;

namespace WhackAMole
{
    public class GamePage : PageBase
    {
        #region Game Signals Handling

        protected override void subscribeSignals()
        {
            SignalBus.Subscribe<GameStartedSignal>(Show);
            SignalBus.Subscribe<GameWonSignal>(Hide);
            SignalBus.Subscribe<GameLostSignal>(Hide);
            SignalBus.Subscribe<GameUnloadedSignal>(Hide);

        }
        protected override void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<GameLoadedSignal>(Show);
            SignalBus.TryUnsubscribe<GameStartedSignal>(Hide);
            SignalBus.TryUnsubscribe<GameLostSignal>(Hide);
            SignalBus.TryUnsubscribe<GameUnloadedSignal>(Hide);
        }

        #endregion
    }
}

