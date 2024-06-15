using Sirenix.OdinInspector;
using UnityEngine;

namespace WhackAMole
{
    public abstract class TimerActionBase : MonoBehaviour
    {
        [FoldoutGroup("References")]
        [SerializeField, Required, ReadOnly] Timer m_Target;

        #region Mono Callbacks
        private void OnEnable()
        {
            m_Target.OnTimesOff.AddListener(OnTimerOff);
        }
        private void OnDisable()
        {
            tryUnsubscribe();
        }
        private void OnDestroy()
        {
            
        }
        private void OnValidate()
        {
            resolveDependencies();
        }
        #endregion
        
        
        private void tryUnsubscribe()
        {
            if (m_Target)
                m_Target.OnTimesOff.RemoveListener(OnTimerOff);
        }


        protected abstract void OnTimerOff();


        [Button, FoldoutGroup("References")]
        private void resolveDependencies()
        {
            m_Target = GetComponent<Timer>();
        }
    }
}

