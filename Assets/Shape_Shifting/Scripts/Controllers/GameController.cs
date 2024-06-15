using System;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public struct GameState
    {
        public bool IsGameLoaded;
        public bool IsGameStarted;
        public bool IsGamePaused;
        public bool IsGameWon;
        public bool IsGameLost;

        public void Reset()
        {
            IsGameLoaded = false;
            IsGameStarted = false;
            IsGamePaused = false;
            IsGameWon = false;
            IsGameLost = false;
        }
    }
    public class GameController : IInitializable, IDisposable
    {
        #region Fields
        [Inject]
        private SignalBus m_SignalBus;
        [Inject]
        private LevelController m_LevelController;
        [Inject]
        private GameControllerSettings m_Settings;
        private GameState m_GameState;
        [Inject]
        WarningPopup m_WarningPopup;

        #endregion

        #region Lifecycle Methods

        public void Initialize()
        {
            Application.targetFrameRate = m_Settings.TargetFrameRate;
            initializeConditions();
            subscribeSignals();
            tryLoadSelectedLevel();
        }
        public void Dispose()
        {
            unsubscribeSignals();
            releaseConditions();
        }

        #endregion

        #region Condition Handling

        private void initializeConditions()
        {
            m_Settings.LoseGameCondition.OnConditionMet += loseGame;
            m_Settings.LoseGameCondition.Initialize();

            m_Settings.WinGameCondition.OnConditionMet += winGame;
            m_Settings.WinGameCondition.Initialize();
        }

        private void releaseConditions()
        {
            m_Settings.LoseGameCondition.OnConditionMet -= loseGame;
            m_Settings.LoseGameCondition.Release();

            m_Settings.WinGameCondition.OnConditionMet -= winGame;
            m_Settings.WinGameCondition.Release();
        }
        #endregion

        #region Signals Handling

        private void subscribeSignals()
        {
            m_SignalBus.Subscribe<StartGameCommandSignal>(startGame);
            m_SignalBus.Subscribe<PauseGameCommandSignal>(pauseGame);
            m_SignalBus.Subscribe<ResumeGameCommandSignal>(resumeGame);
            m_SignalBus.Subscribe<WinGameCommandSignal>(winGame);
            m_SignalBus.Subscribe<LoseGameCommandSignal>(loseGame);
            m_SignalBus.Subscribe<UnloadGameCommandSignal>(onUnloadGameSignal);
            m_SignalBus.Subscribe<RestartGameCommandSignal>(restartGame);
            m_SignalBus.Subscribe<LoadNextLevelCommandSignal>(loadNext);
            m_SignalBus.Subscribe<LevelSelectedSignal>(onLevelSelectedSignal);
        }
        private void unsubscribeSignals()
        {
            m_SignalBus.TryUnsubscribe<StartGameCommandSignal>(startGame);
            m_SignalBus.TryUnsubscribe<PauseGameCommandSignal>(pauseGame);
            m_SignalBus.TryUnsubscribe<ResumeGameCommandSignal>(resumeGame);
            m_SignalBus.TryUnsubscribe<WinGameCommandSignal>(winGame);
            m_SignalBus.TryUnsubscribe<LoseGameCommandSignal>(loseGame);
            m_SignalBus.TryUnsubscribe<UnloadGameCommandSignal>(onUnloadGameSignal);
            m_SignalBus.TryUnsubscribe<RestartGameCommandSignal>(restartGame);
            m_SignalBus.TryUnsubscribe<LoadNextLevelCommandSignal>(loadNext);
            m_SignalBus.TryUnsubscribe<LevelSelectedSignal>(onLevelSelectedSignal);
        }
        private void onLevelSelectedSignal(LevelSelectedSignal i_Signal)
        {
            unloadGame(false);
            loadGame(i_Signal.LevelData);
        }
        private void onUnloadGameSignal()
        {
            unloadGame();
        }
        #endregion

        #region Game Methods

        private void tryLoadSelectedLevel()
        {
            if (m_LevelController.GetSelectedLevelData(out LevelData o_LevelData))
                loadGame(o_LevelData);
#if UNITY_EDITOR
            else
                Debug.LogError("[Game Controller] Couldn't find selected level");
#endif
        }

        private void loadNext()
        {
            m_LevelController.SelectNextLevel();
            startGame();
        }

        private void loadGame(LevelData i_LevelData)
        {
            if (m_GameState.IsGameLoaded)
                return;
            m_GameState.IsGameLoaded = true;

            m_SignalBus.TryFire(new GameLoadedSignal(i_LevelData));
        }
        private void unloadGame(bool i_AutoReload = true)
        {
            if (!m_GameState.IsGameLoaded)
                return;

            m_GameState.Reset();

            m_SignalBus.TryFire<GameUnloadedSignal>();

            if (i_AutoReload)
                tryLoadSelectedLevel();
        }
        private void startGame()
        {
            if (!m_GameState.IsGameLoaded || m_GameState.IsGameStarted)
                return;

            if (m_Settings.StartGameCondition && !m_Settings.StartGameCondition.CheckCondition(out string o_ErrorMessage))
            {
                m_WarningPopup.Pop(o_ErrorMessage);
                return;
            }

            m_GameState.IsGameStarted = true;

            m_SignalBus.TryFire<GameStartedSignal>();
        }

        private void restartGame()
        {
            unloadGame();
            startGame();
        }

        private void loseGame()
        {
            if (!m_GameState.IsGameLoaded || !m_GameState.IsGameStarted || m_GameState.IsGameLost)
                return;

            m_GameState.IsGameLost = true;

            m_SignalBus.TryFire<GameLostSignal>();
        }
        private void winGame()
        {
            if (!m_GameState.IsGameLoaded || !m_GameState.IsGameStarted || m_GameState.IsGameWon)
                return;

            m_GameState.IsGameWon = true;

            m_SignalBus.TryFire<GameWonSignal>();
        }
        private void pauseGame()
        {
            if (!m_GameState.IsGameLoaded || !m_GameState.IsGameStarted || m_GameState.IsGamePaused)
                return;

            m_GameState.IsGamePaused = true;

            m_SignalBus.TryFire<GamePausedSignal>();
        }
        private void resumeGame()
        {
            if (!m_GameState.IsGameLoaded || !m_GameState.IsGameStarted || !m_GameState.IsGamePaused)
                return;

            m_GameState.IsGamePaused = false;

            m_SignalBus.TryFire<GameResumedSignal>();
        }

        #endregion
    }

}
