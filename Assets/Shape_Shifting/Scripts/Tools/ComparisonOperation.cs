using UnityEngine;

namespace ShapeShifting
{
    [System.Serializable]
    public class ComparisonOperation
    {
        [SerializeField] private eCompareOperationMethod m_ComparisonOperation;
        [SerializeField] private int m_CompareValue;

        public bool Compare(int i_Value)
        {
            switch (m_ComparisonOperation)
            {
                case eCompareOperationMethod.Equal:
                    return i_Value == m_CompareValue;
                case eCompareOperationMethod.Less:
                    return i_Value < m_CompareValue;
                case eCompareOperationMethod.More:
                    return i_Value > m_CompareValue;
                case eCompareOperationMethod.MoreOrEqual:
                    return i_Value >= m_CompareValue;
                default:
                    return i_Value <= m_CompareValue;
            }
        }
    }
}