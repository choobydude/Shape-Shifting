using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{

    [CreateAssetMenu(fileName = "New Record Model", menuName = "Models/Record Model")]
    public class RecordModel : ScriptableObject
    {
        #region Fields
        [Inject]
        SignalBus m_SignalBus;

        [field: SerializeField] public eRecordType Type { get; private set; }

        [ShowInInspector, ReadOnly] private int m_Value;
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
            m_SignalBus.TryFire(new RecordChangedSignal(Type, m_Value));
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
        public bool HasAmount(int i_Amount)
        {
            return Value >= i_Amount;
        }
        public void Clear()
        {
            Value = 0;
        }

        #endregion
    }
}


