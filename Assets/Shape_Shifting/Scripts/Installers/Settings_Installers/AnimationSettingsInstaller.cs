using UnityEngine;
using ShapeShifting;
using Zenject;

[CreateAssetMenu(fileName = "New Animation Settings Installer", menuName = "Installers/Animation Settings Installer")]
public class AnimationSettingsInstaller : ScriptableObjectInstaller<AnimationSettingsInstaller>
{
    [SerializeField] private ButtonAnimationSettings m_Settings;

    public override void InstallBindings()
    {
        Container.BindInstance(m_Settings);
    }
}