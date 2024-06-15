using TMPro;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class LevelView : SignalButtonBase
    {

        #region Serialized Fields
        [SerializeField] private TextMeshProUGUI m_NameText;
        [SerializeField] private GameObject m_LockedVisuals;
        [SerializeField] private GameObject m_SelectedVisuals;

        private LevelData m_LastUpdatedLevelData;

        #endregion

        #region Lifecycle Methods
        private void onSpawned(LevelData i_LevelModelData, Transform i_Parent)
        {
            transform.SetParent(i_Parent);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            setLevelData(i_LevelModelData);
            subscribeLevelModifiedSignal();
        }
        private void onDespawn()
        {
            unsubscribeLevelModifiedSignal();
        }
        #endregion

        #region Signal Handling

        private void subscribeLevelModifiedSignal()
        {
            SignalBus.Subscribe<LevelModifiedSignal>(onLevelModified);
        }

        private void unsubscribeLevelModifiedSignal()
        {
            SignalBus.TryUnsubscribe<LevelModifiedSignal>(onLevelModified);
        }

        #endregion

        #region Button Methods

        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire(new SelectLevelCommandSignal(m_LastUpdatedLevelData.Name));
        }

        #endregion

        #region Visual Update Methods
        private void setLevelData(LevelData i_LevelData)
        {
            m_LastUpdatedLevelData = i_LevelData;
            updateView();
        }
        private void onLevelModified(LevelModifiedSignal i_Signal)
        {
            updateLevelData(i_Signal.LevelData);
        }
        private void updateLevelData(LevelData i_LevelData)
        {
            if (i_LevelData.Name != m_LastUpdatedLevelData.Name)
                return;

            setLevelData(i_LevelData);
        }
        private void updateView()
        {
            setName(m_LastUpdatedLevelData.Name);
            setLocked(m_LastUpdatedLevelData.Islocked);
            setSelected(m_LastUpdatedLevelData.IsSelected);
        }
        
        private void setName(string i_Name)
        {
            m_NameText.text = i_Name;
        }
        private void setLocked(bool i_IsLocked)
        {
            m_LockedVisuals.SetActive(i_IsLocked);
            m_SelectedVisuals.SetActive(!i_IsLocked);
        }
        private void setSelected(bool i_IsSelected)
        {
            m_SelectedVisuals.SetActive(i_IsSelected);
        }
        #endregion

        #region Pooling

        public class Pool : MonoMemoryPool<LevelData,Transform, LevelView>
        {
            protected override void Reinitialize(LevelData i_LevelModelData,Transform i_Parent, LevelView i_LevelView)
            {
                base.Reinitialize(i_LevelModelData, i_Parent, i_LevelView);
                i_LevelView.onSpawned(i_LevelModelData, i_Parent);
            }
            protected override void OnDespawned(LevelView i_LevelView)
            {
                base.OnDespawned(i_LevelView);
                i_LevelView.onDespawn();
            }
        }

        #endregion
    }
}
