using System;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public class GameEventsLogger : LoggerBase
    {
        #region Constructors

        public GameEventsLogger() : base("Game Event", Color.red)
        {

        }

        #endregion

        #region Lifecycle Methods

        public override void Initialize()
        {
            subscribeGameSignals();
        }
        public override void Dispose()
        {
            unsubscribeGameSignals();
        }

        #endregion

        #region Signal Handling
        private void subscribeGameSignals()
        {
            SignalBus.Subscribe<GameLoadedSignal>(LogGameLoaded);
            SignalBus.Subscribe<GameUnloadedSignal>(LogGameUnloaded);
            SignalBus.Subscribe<GameStartedSignal>(LogGameStarted);
            SignalBus.Subscribe<GameLostSignal>(LogGameLost);
            SignalBus.Subscribe<GameWonSignal>(LogGameWon);
            SignalBus.Subscribe<GamePausedSignal>(LogGamePaused);
            SignalBus.Subscribe<GameResumedSignal>(LogGameResumed);
        }
        private void unsubscribeGameSignals()
        {
            SignalBus.TryUnsubscribe<GameLoadedSignal>(LogGameLoaded);
            SignalBus.TryUnsubscribe<GameUnloadedSignal>(LogGameUnloaded);
            SignalBus.TryUnsubscribe<GameStartedSignal>(LogGameStarted);
            SignalBus.TryUnsubscribe<GameLostSignal>(LogGameLost);
            SignalBus.TryUnsubscribe<GameWonSignal>(LogGameWon);
            SignalBus.TryUnsubscribe<GamePausedSignal>(LogGamePaused);
            SignalBus.TryUnsubscribe<GameResumedSignal>(LogGameResumed);
        }
        #endregion

        #region Logging

        private void LogGameLoaded(GameLoadedSignal i_Signal) => Log("On Game Loaded");
        private void LogGameUnloaded(GameUnloadedSignal i_Signal) => Log("On Game Unloaded");
        private void LogGameStarted(GameStartedSignal i_Signal) => Log("On Game Started");
        private void LogGameWon(GameWonSignal i_Signal) => Log($"On Game Won");
        private void LogGameLost(GameLostSignal i_Signal) => Log("On Game Lost");
        private void LogGamePaused(GamePausedSignal i_Signal) => Log("On Game Paused");
        private void LogGameResumed(GameResumedSignal i_Signal) => Log("On Game Resumed");

        #endregion
    }
}

