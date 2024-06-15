using System;
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
        protected override void Awake()
        {
            subscribeSignals();
            base.Awake();
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

