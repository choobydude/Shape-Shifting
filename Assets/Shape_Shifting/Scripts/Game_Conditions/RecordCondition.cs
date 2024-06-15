using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Record Condition", menuName = "Conditions/Record Condition")]
    public class RecordCondition : ConditionBase
    {
        [Inject]
        RecordController m_RecordController;
        [SerializeField] private eRecordType m_RecordType;
        [SerializeField] private ComparisonOperation m_CompareOperation;

        public override void Initialize()
        {
            SignalBus.Subscribe<RecordChangedSignal>(onRecordChanged);
        }
        public override void Release()
        {
            SignalBus.TryUnsubscribe<RecordChangedSignal>(onRecordChanged);
        }

        private void onRecordChanged(RecordChangedSignal i_Signal)
        {
            if (i_Signal.RecordType == m_RecordType)
            {
                if (CheckCondition(out string o_ErrorMessage))
                    ConditionsMet();
            }
        }

        public override bool CheckCondition(out string o_ErrorMessage)
        {
            if (m_RecordController.GetRecordValue(m_RecordType, out int o_Value))
            {
                bool result = m_CompareOperation.Compare(o_Value);
                o_ErrorMessage = result ? "success" : ErrorMessage;
                return result;
            }

            o_ErrorMessage = "value not fount";
            return false;
        }
    }
}

