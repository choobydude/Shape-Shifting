using System.Collections;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Tool Controller Settings Installer", menuName = "Installers/Tool Controller Settings Installer")]
    public class ToolControllerSettingsInstaller : ScriptableObjectInstaller<LevelControllerSettingsInstaller>
    {
        [SerializeField] private ToolControllerSettings m_Settings;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Settings);
            m_Settings.Tools.ForEach(model => Container.QueueForInject(model));
        }
    }
}