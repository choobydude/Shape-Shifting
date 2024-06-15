using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public class LevelBrowserPage : PageBase
    {
        #region Serialized Fields

        [SerializeField] private Transform m_LevelViewContainer;

        #endregion

        #region Non Serialized Fields

        [Inject]
        private LevelView.Pool m_LevelViewPool;

        #endregion

        #region Signals Handling
        protected override void subscribeSignals()
        {
            SignalBus.Subscribe<LevelsInitializedSignal>(createLevelViews);
            SignalBus.Subscribe<GameStartedSignal>(Hide);
            SignalBus.Subscribe<OpenLevelBrowserPageCommandSignal>(Show);
            SignalBus.Subscribe<CloseLevelBrowserPageCommandSignal>(Hide);
        }

        protected override void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<LevelsInitializedSignal>(createLevelViews);
            SignalBus.TryUnsubscribe<GameStartedSignal>(Hide);
            SignalBus.TryUnsubscribe<OpenLevelBrowserPageCommandSignal>(Show);
            SignalBus.TryUnsubscribe<CloseLevelBrowserPageCommandSignal>(Hide);
        }

        #endregion

        #region Create Content
        private void createLevelViews(LevelsInitializedSignal i_Signal)
        {
            List<LevelData> levelModels = i_Signal.LevelsData;
            for (int i = 0; i < levelModels.Count; i++)
                m_LevelViewPool.Spawn(levelModels[i], m_LevelViewContainer);
        }
        #endregion
    }
}

