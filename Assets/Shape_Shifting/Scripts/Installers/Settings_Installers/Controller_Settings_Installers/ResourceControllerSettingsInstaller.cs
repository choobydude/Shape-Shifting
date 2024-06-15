using UnityEngine;
using Zenject;

namespace WhackAMole
{
    [CreateAssetMenu(fileName = "New Resource Controller Settings Installer", menuName = "Installers/Resource Controller Settings Installer")]
    public class ResourceControllerSettingsInstaller : ScriptableObjectInstaller<ResourceControllerSettingsInstaller>
    {
        [SerializeField] private ResourceControllerSettings m_Settings;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Settings);
            m_Settings.Resources.ForEach(model => Container.QueueForInject(model));
        }
    }
}