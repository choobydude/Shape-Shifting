using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Level Model", menuName = "Models/Level Model")]
    public class LevelModel : ScriptableObject
    {
        #region Fields
        [field: SerializeField] public LevelData Data { get; private set; }
        [Inject]
        SignalBus m_SignalBus;
        #endregion

        #region Events

        private void fireLevelModifiedSignal()
        {
            m_SignalBus.TryFire(new LevelModifiedSignal(Data));
        }
        private void fireLevelSelectedSignal()
        {
            m_SignalBus.TryFire(new LevelSelectedSignal(Data));
        }

        #endregion

        #region Interaction

        public void Select()
        {
            if (Data.IsSelected || Data.Islocked)
                return;

            LevelData modifiedData = Data;
            modifiedData.IsSelected = true;
            Data = modifiedData;

            fireLevelModifiedSignal();
            fireLevelSelectedSignal();
        }

        public void Deselect()
        {
            if (!Data.IsSelected)
                return;

            LevelData modifiedData = Data;
            modifiedData.IsSelected = false;
            Data = modifiedData;

            fireLevelModifiedSignal();
        }

        public void Unlock()
        {
            if (!Data.Islocked)
                return;

            LevelData modifiedData = Data;
            modifiedData.Islocked = false;
            Data = modifiedData;

            fireLevelModifiedSignal();
        }

        #endregion
    }

    [System.Serializable]
    public struct LevelData
    {
        public string Name;
        public bool IsSelected;
        public bool Islocked;

        public int SpawnPointCount;
        [MinMaxSlider(0.2f, 2)] public Vector2 MinMaxSpawnInterval;
        public Vector2 SpawnArea;
        public float SpawnPointRadius;
    }
}

