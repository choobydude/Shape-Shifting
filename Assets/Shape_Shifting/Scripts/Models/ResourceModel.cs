using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Resource Model", menuName = "Models/Resource Model")]
    public class ResourceModel : ScriptableObject
    {
        #region Fields
        [Inject]
        SignalBus m_SignalBus;

        [field: SerializeField, PreviewField] public Sprite Icon { get; private set; }
        [field: SerializeField] public eResourceType Type { get; private set; }

        [SerializeField] private int m_Value;
        public int Value
        {
            get
            {
                return m_Value;
            }
            private set
            {
                bool changeDetected = m_Value != value;
                m_Value = value;
                if (changeDetected)
                    fireChangeEvent();
            }
        }

        #endregion

        #region Events

        private void fireChangeEvent()
        {
            m_SignalBus.TryFire(new ResourceChangedSignal(Type, m_Value));
        }

        #endregion

        #region Interaction Methods

        [Button, FoldoutGroup("Manual Control")]
        public void Add(int i_Amount)
        {
            if (i_Amount <= 0)
                return;

            Value += i_Amount;
        }
        [Button, FoldoutGroup("Manual Control")]
        public void Remove(int i_Amount)
        {
            if (i_Amount <= 0 || i_Amount > m_Value)
                return;

            Value -= i_Amount;
        }
        public bool HasAmount(int i_Amount)
        {
            return Value >= i_Amount;
        }

        #endregion
    }
}
