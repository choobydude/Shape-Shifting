using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Record Controller Settings Installer", menuName = "Installers/Record Controller Settings Installer")]
    public class RecordControllerSettingsInstaller : ScriptableObjectInstaller<RecordControllerSettingsInstaller>
    {
        [SerializeField] private RecordControllerSettings m_Settings;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Settings);
            m_Settings.Records.ForEach(model => Container.QueueForInject(model));
        }
    }
}