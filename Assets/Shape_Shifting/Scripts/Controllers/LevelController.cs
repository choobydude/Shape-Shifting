using Zenject;
using System.Linq;
using System;
using UnityEngine;

namespace ShapeShifting
{
    public class LevelController : IInitializable, IDisposable
    {
        #region Non Serialized Fields
        [Inject]
        LevelControllerSettings m_Settings;
        [Inject]
        SignalBus m_SignalBus;
        LevelModel m_Level;

        #endregion

        #region Lifecycle Methods

        public void Initialize()
        {
            subscribeCommandSignals();
            m_SignalBus.TryFire(new LevelsInitializedSignal(m_Settings.LevelModels.Select(model => model.Data).ToList()));
            getSelectedLevel().Select();
        }

        public void Dispose()
        {
            unsubscribeCommandSignals();
        }
        #endregion

        #region Signal Handling

        private void subscribeCommandSignals()
        {
            m_SignalBus.Subscribe<SelectLevelCommandSignal>(trySelectLevel);
            m_SignalBus.Subscribe<LevelSelectedSignal>(onLevelSelected);
            m_SignalBus.Subscribe<GameUnloadedSignal>(unloadLevel);
            m_SignalBus.Subscribe<GameStartedSignal>(onGameStarted);

        }

        private void unsubscribeCommandSignals()
        {
            m_SignalBus.TryUnsubscribe<SelectLevelCommandSignal>(trySelectLevel);
            m_SignalBus.TryUnsubscribe<LevelSelectedSignal>(onLevelSelected);
            m_SignalBus.TryUnsubscribe<GameUnloadedSignal>(unloadLevel);
            m_SignalBus.TryUnsubscribe<GameStartedSignal>(onGameStarted);
        }

        private void onGameStarted()
        {
            if (m_Level)
                return;

            cleanPreviousLevel();

            m_Level = getSelectedLevel();
            if (m_Level)
                m_Level.CreateLevel();
        }
        private void unloadLevel()
        {
            if (m_Level)
                m_Level.ClearLevel();
            getSelectedLevel().ClearLevel();
            m_Level = null;
        }

        #endregion

        #region Level Control Methods

        private void onLevelSelected(LevelSelectedSignal i_Signal)
        {
            cleanPreviousLevel();

            m_Level = getSelectedLevel();
            if(m_Level)
                m_Level.CreateLevel();
        }

        private void cleanPreviousLevel()
        {
            m_Level?.ClearLevel();
        }

        private LevelModel getSelectedLevel()
        {
            return m_Settings.LevelModels.FirstOrDefault(model => model.Data.IsSelected);
        }

        private void trySelectLevel(SelectLevelCommandSignal i_Signal)
        {
            LevelModel levelModel = getLevelByName(i_Signal.LevelName);
            if (!levelModel || levelModel.Data.Islocked || levelModel.Data.IsSelected)
                return;

            deselectSelectedLevel();
            levelModel.Select();
        }
        private void deselectSelectedLevel()
        {
            m_Settings.LevelModels.FirstOrDefault(model => model.Data.IsSelected)?.Deselect();
        }
        private void selectLevelByName(string i_LevelName)
        {
            m_Settings.LevelModels.FirstOrDefault(model => model.Data.Name.Equals(i_LevelName))?.Select();
        }
        private LevelModel getLevelByName(string i_LevelName)
        {
            return m_Settings.LevelModels.FirstOrDefault(model => model.Data.Name.Equals(i_LevelName));
        }

        public bool GetSelectedLevelData(out LevelData i_Data)
        {
            LevelModel selectedLevel = m_Settings.LevelModels.FirstOrDefault(model => model.Data.IsSelected);
            if (!selectedLevel)
            {
                i_Data = default;
                return false;
            }
            i_Data = selectedLevel.Data;
            return true;
        }

        private LevelModel getNextUnselectedLevel()
        {
            int selectedIndex = m_Settings.LevelModels.FindIndex(model => model.Data.IsSelected);
            if (selectedIndex < 0 || selectedIndex + 1 >= m_Settings.LevelModels.Count)
                return null;

            return m_Settings.LevelModels[selectedIndex + 1];
        }
        public void SelectNextLevel()
        {
            LevelModel nextUnselectedLevel = getNextUnselectedLevel();
            if (!nextUnselectedLevel)
                return;

            deselectSelectedLevel();

            if (nextUnselectedLevel.Data.Islocked)
                nextUnselectedLevel.Unlock();

            nextUnselectedLevel.Select();
        }
        #endregion
    }
}

