using UnityEngine;
using ShapeShifting;
using Zenject;

[CreateAssetMenu(fileName = "New Decoration Settings Installer", menuName = "Installers/Decoration Settings Installer")]
public class DecorationSettingsInstaller : ScriptableObjectInstaller<DecorationSettingsInstaller>
{
    [SerializeField] private ButtonSaturationSettings m_Settings;

    public override void InstallBindings()
    {
        Container.BindInstance(m_Settings);
    }
}