using System;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public abstract class PageBase : UIGroupBase
    {
        #region Fields
        [Inject]
        protected SignalBus SignalBus;

        #endregion

        #region Lifecycle Methods
        public void Setup()
        {
            subscribeSignals();
        }
        private void OnDestroy()
        {
            unsubscribeSignals();
        }
        #endregion

        #region Signals Handling

        protected abstract void subscribeSignals();
        protected abstract void unsubscribeSignals();
        #endregion

    }
}

