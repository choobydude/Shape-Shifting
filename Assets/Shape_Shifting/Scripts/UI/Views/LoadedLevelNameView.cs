using TMPro;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public class LoadedLevelNameView : UIElementBase
    {
        #region Fields
        [Inject]
        SignalBus m_SignalBus;
        [SerializeField] string m_TextPrefix = "Level ";
        [SerializeField] TextMeshProUGUI m_Text;
        [Inject]
        LevelController m_LevelController;
        #endregion

        #region Mono Methods
        private void OnEnable()
        {
            manualUpdate();
            m_SignalBus.Subscribe<GameLoadedSignal>(onLevelLoaded);
        }

        private void OnDisable()
        {
            m_SignalBus.TryUnsubscribe<GameLoadedSignal>(onLevelLoaded);
        }

        private void manualUpdate()
        {
            if (m_LevelController.GetSelectedLevelData(out LevelData o_LevelData))
                setText(o_LevelData.Name);
        }
        #endregion

        #region Text Handling
        private void onLevelLoaded(GameLoadedSignal i_Signal)
        {
            setText(i_Signal.LevelData.Name);
        }

        private void setText(string i_LevelName)
        {
            m_Text.text = m_TextPrefix + i_LevelName;
        }
        #endregion
    }
}

