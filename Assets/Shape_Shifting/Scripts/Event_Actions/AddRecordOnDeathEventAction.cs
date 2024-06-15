using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public class AddRecordOnDeathEventAction : MonoBehaviour
    {
        [SerializeField] private eRecordType m_RecordType;
        [SerializeField] private int i_Amount;
        [Inject]
        RecordController m_RecordController;

        IKillable m_Target;

        private void OnEnable()
        {
            m_Target = GetComponent<IKillable>();
            if (m_Target != null)
                m_Target.OnDeath += addRecord;
        }
        private void OnDisable()
        {
            if (m_Target != null)
                m_Target.OnDeath -= addRecord;
        }
        private void OnDestroy()
        {
            if (m_Target != null)
                m_Target.OnDeath -= addRecord;
        }

        private void addRecord()
        {
            m_RecordController.AddRecord(m_RecordType, i_Amount);
        }
    }
}

