using UnityEngine;
using Zenject;

namespace WhackAMole
{
    [CreateAssetMenu(fileName = "New Level Controller Settings Installer", menuName = "Installers/Level Controller Settings Installer")]
    public class LevelControllerSettingsInstaller : ScriptableObjectInstaller<LevelControllerSettingsInstaller>
    {
        [SerializeField] private LevelControllerSettings m_Settings;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Settings);
            m_Settings.LevelModels.ForEach(model => Container.QueueForInject(model));
        }
    }
}
