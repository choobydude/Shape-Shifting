using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ShapeShifting
{
    public class Timer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float m_Lifespan;
        [SerializeField] private bool m_AutoRestart;

        private float m_RemainingTime;
        
        #endregion

        #region Events

        [FoldoutGroup("Events")]public UnityEvent<float, float> OnTimerTick;
        [FoldoutGroup("Events")] public UnityEvent OnTimesOff;

        #endregion

        #region Protected Methods

        protected virtual void Tick(float i_RemainingTime)
        {
            fireTickEvent();
        }
        protected virtual void TimesOff()
        {
            fireOffEvent();

            if (m_AutoRestart)
                RestartTimer();
        }


        #endregion

        #region Control Methods
        public void StartTimer()
        {
            m_RemainingTime = m_Lifespan;

            StopAllCoroutines();
            StartCoroutine(timerCoroutine());
        }
        public void StopTimer()
        {
            m_RemainingTime = 0;

            StopAllCoroutines();
        }
        public void PauseTimer()
        {
            StopAllCoroutines();
        }
        public void ResumeTimer()
        {
            StopAllCoroutines();
            StartCoroutine(timerCoroutine());
        }
        public void RestartTimer()
        {
            StartTimer();
            fireTickEvent();
        }
        #endregion

        #region Event Firing
        private void fireTickEvent()
        {
            OnTimerTick?.Invoke(m_RemainingTime, m_Lifespan);
        }
        private void fireOffEvent()
        {
            OnTimesOff?.Invoke();
        }
        #endregion

        #region Mono Methods

        
        private void OnDisable()
        {
            StopAllCoroutines();
        }

        #endregion

        #region Coroutine
        
        IEnumerator timerCoroutine()
        {
            while (m_RemainingTime > 0)
            {
                yield return new WaitForSeconds(1);
                m_RemainingTime -= 1;
                Tick(m_RemainingTime);
            }

            TimesOff();
        }

        #endregion
    }
}

