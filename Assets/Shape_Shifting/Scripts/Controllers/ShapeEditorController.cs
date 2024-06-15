using Cinemachine;
using System;
using Zenject;

namespace ShapeShifting
{
    public class ShapeEditorController : IInitializable, IDisposable
    {
        [Inject]
        SignalBus m_SignalBus;

        public void Initialize()
        {
            subscribeSignals();
        }
        public void Dispose()
        {
            unsubscribeSignals();
        }


        private void subscribeSignals()
        {
            m_SignalBus.Subscribe<GameStartedSignal>(onGameStarted);
            m_SignalBus.Subscribe<ExitShapeEditorCommandSignal>(onExitShapeEditorCommand);
        }
        private void unsubscribeSignals()
        {
            m_SignalBus.TryUnsubscribe<GameStartedSignal>(onGameStarted);
            m_SignalBus.TryUnsubscribe<ExitShapeEditorCommandSignal>(onExitShapeEditorCommand);
        }

        private void onGameStarted()
        {
            m_SignalBus.TryFire<ShapeEditorEnteredSignal>();
        }
        private void onExitShapeEditorCommand()
        {
            m_SignalBus.TryFire<ExitShapeEditorCommandSignal>();
        }
    }
}