using Sirenix.OdinInspector;
using UnityEngine;

namespace ShapeShifting
{
    public class AutoDespawnTimer : Timer
    {
        [FoldoutGroup("References")]
        [SerializeField, Required, ReadOnly] SpawnObject m_Target;

        private void OnEnable()
        {
            StartTimer();
        }
        private void OnDisable()
        {
            StopTimer();
        }

        private void OnValidate()
        {
            resolveDependencies();
        }

        protected override void TimesOff()
        {
            base.TimesOff();
            m_Target.Despawn();
        }
        [Button,FoldoutGroup("References")]
        private void resolveDependencies()
        {
            m_Target = GetComponent<SpawnObject>();
        }
    }
}

