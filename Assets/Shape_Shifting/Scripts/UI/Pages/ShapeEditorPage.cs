using UnityEngine;

namespace ShapeShifting
{
    public class ShapeEditorPage : PageBase
    {
        protected override void subscribeSignals()
        {
            SignalBus.Subscribe<GameStartedSignal>(Show);
            SignalBus.Subscribe<GameWonSignal>(Hide);
            SignalBus.Subscribe<GameLostSignal>(Hide);
            SignalBus.Subscribe<GameUnloadedSignal>(Hide);
        }

        protected override void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<GameStartedSignal>(Show);
            SignalBus.TryUnsubscribe<GameWonSignal>(Hide);
            SignalBus.TryUnsubscribe<GameLostSignal>(Hide);
            SignalBus.TryUnsubscribe<GameUnloadedSignal>(Hide);
        }
    }
}

