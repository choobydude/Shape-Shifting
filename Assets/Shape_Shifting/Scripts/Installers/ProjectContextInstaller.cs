using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New Project Context Installer", menuName = "Installers/Project Context Installer")]
public class ProjectContextInstaller : ScriptableObjectInstaller<ProjectContextInstaller>
{
    public override void InstallBindings()
    {
        installSignalBus();
    }

    private void installSignalBus()
    {
        SignalBusInstaller.Install(Container);
    }
}