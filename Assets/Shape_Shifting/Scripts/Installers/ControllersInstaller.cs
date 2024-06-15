using Zenject;

namespace WhackAMole
{
    public class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ResourceController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RecordController>().AsSingle().NonLazy();
        }
    }
}
