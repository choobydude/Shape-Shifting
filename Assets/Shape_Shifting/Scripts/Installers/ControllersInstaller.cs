using Zenject;

namespace ShapeShifting
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
            Container.BindInterfacesAndSelfTo<ToolController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShapeEditorController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIController>().AsSingle().NonLazy();


            Container.BindExecutionOrder<UIController>(-10);
            Container.BindExecutionOrder<GameController>(-5);
        }
    }
}
