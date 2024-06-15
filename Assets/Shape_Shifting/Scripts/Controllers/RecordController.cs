using System;
using System.Linq;
using Zenject;

namespace WhackAMole
{
    public class RecordController : IInitializable, IDisposable
    {
        #region Fields

        [Inject]
        RecordControllerSettings m_Settings;
        [Inject]
        SignalBus m_SignalBus;


        #endregion

        #region Lifecycle

        public void Initialize()
        {
            subsribeSignals();
        }

        public void Dispose()
        {
            unsubscribeSignal();
        }

        #endregion

        #region Signal Handling

        private void subsribeSignals()
        {
            m_SignalBus.Subscribe<GameStartedSignal>(clearCurrentLevelKillsRecord);
        }
        private void unsubscribeSignal()
        {
            m_SignalBus.TryUnsubscribe<GameStartedSignal>(clearCurrentLevelKillsRecord);
        }

        private void clearCurrentLevelKillsRecord()
        {
            ClearRecord(eRecordType.CurrentLevelKills);
        }

        #endregion

        #region Interaction Methods

        private bool getRecord(eRecordType i_RecordType, out RecordModel i_Record)
        {
            i_Record = m_Settings.Records.FirstOrDefault(x => x.Type == i_RecordType);
            return i_Record;
        }
        public bool GetRecordValue(eRecordType i_RecordType, out int o_Value)
        {
            if (getRecord(i_RecordType, out RecordModel o_Record))
            {
                o_Value = o_Record.Value;
                return true;
            }
            o_Value = default;
            return false;
        }
        public bool HasEnoughRecord(eRecordType i_RecordType, int i_Amount)
        {
            if (getRecord(i_RecordType, out RecordModel o_Record))
                return o_Record.HasAmount(i_Amount);
            return false;
        }
        public void AddRecord(eRecordType i_RecordType, int i_Amount)
        {
            if (getRecord(i_RecordType, out RecordModel o_Record))
                o_Record.Add(i_Amount);
        }
        public void ClearRecord(eRecordType i_RecordType)
        {
            if (getRecord(i_RecordType, out RecordModel o_Record))
                o_Record.Clear();
        }

        
        #endregion
    }
}

