using Zenject;

namespace ShapeShifting
{
    public class MainPage : PageBase
    {
        #region Game Signals Handling
        protected override void subscribeSignals()
        {
            SignalBus.Subscribe<GameLoadedSignal>(Show);
            SignalBus.Subscribe<GameStartedSignal>(Hide);
        }
        protected override void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<GameLoadedSignal>(Show);
            SignalBus.TryUnsubscribe<GameStartedSignal>(Hide);
        }

        #endregion
    }
}

