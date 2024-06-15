using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Log Settings Installer", menuName = "Installers/Log Settings Installer")]
    public class LogSettingsInstaller : ScriptableObjectInstaller<LogSettingsInstaller>
    {
        [SerializeField] private LogSettings m_Settings;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Settings);
        }
    }
}
