using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class GamePage : PageBase
    {
        [SerializeField] GameObject m_EditorArea;

        #region Game Signals Handling

        protected override void subscribeSignals()
        {
            SignalBus.Subscribe<GameStartedSignal>(Show);
            SignalBus.Subscribe<GameWonSignal>(Hide);
            SignalBus.Subscribe<GameLostSignal>(Hide);
            SignalBus.Subscribe<GameUnloadedSignal>(Hide);
            SignalBus.Subscribe<ShapeEditorEnteredSignal>(onShapeEditingStarted);
            SignalBus.Subscribe<ShapeEditorExitedSignal>(onShapeEditingEnded);
        }
        protected override void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<GameLoadedSignal>(Show);
            SignalBus.TryUnsubscribe<GameStartedSignal>(Hide);
            SignalBus.TryUnsubscribe<GameLostSignal>(Hide);
            SignalBus.TryUnsubscribe<GameUnloadedSignal>(Hide);
            SignalBus.TryUnsubscribe<ShapeEditorEnteredSignal>(onShapeEditingStarted);
            SignalBus.TryUnsubscribe<ShapeEditorExitedSignal>(onShapeEditingEnded);
        }

        #endregion



        private void onShapeEditingStarted()
        {
            m_EditorArea.SetActive(true);
        }
        private void onShapeEditingEnded()
        {
            m_EditorArea.SetActive(false);
        }
    }
}

