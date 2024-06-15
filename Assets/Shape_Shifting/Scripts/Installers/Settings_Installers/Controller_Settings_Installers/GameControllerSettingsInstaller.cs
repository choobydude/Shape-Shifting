using UnityEngine;
using Zenject;

namespace WhackAMole
{
    [CreateAssetMenu(fileName = "New Game Controller Settings Installer", menuName = "Installers/Game Controller Settings Installer")]
    public class GameControllerSettingsInstaller : ScriptableObjectInstaller<LevelControllerSettingsInstaller>
    {
        [SerializeField] private GameControllerSettings m_Settings;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Settings);
            if (m_Settings.StartGameCondition)
                Container.QueueForInject(m_Settings.StartGameCondition);
            if (m_Settings.LoseGameCondition)
                Container.QueueForInject(m_Settings.LoseGameCondition);
            if (m_Settings.WinGameCondition)
                Container.QueueForInject(m_Settings.WinGameCondition);
        }
    }
}