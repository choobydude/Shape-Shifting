using TMPro;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public class RecordView : UIElementBase
    {
        #region Fields
        [Inject]
        SignalBus m_SignalBus;
        [SerializeField] eRecordType m_RecordType;

        [SerializeField] string m_TextPrefix = "Total Score : ";
        [SerializeField] TextMeshProUGUI m_Text;
        [Inject]
        RecordController m_RecordController;
        #endregion

        #region Mono Methods
        private void OnEnable()
        {
            manualUpdate();
            m_SignalBus.Subscribe<RecordChangedSignal>(onRecordChanged);
        }

        private void OnDisable()
        {
            m_SignalBus.TryUnsubscribe<RecordChangedSignal>(onRecordChanged);
        }

        private void manualUpdate()
        {
            if (m_RecordController.GetRecordValue(m_RecordType, out int o_Value))
                setText(o_Value.ToString());
        }
        #endregion

        #region Text Handling
        private void onRecordChanged(RecordChangedSignal i_Signal)
        {
            if (i_Signal.RecordType == m_RecordType)
                setText(i_Signal.Value.ToString());
        }

        private void setText(string i_LevelName)
        {
            m_Text.text = m_TextPrefix + i_LevelName;
        }
        #endregion
    }
}


