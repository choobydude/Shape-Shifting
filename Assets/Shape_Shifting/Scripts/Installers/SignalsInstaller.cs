using WhackAMole;
using Zenject;

public class SignalsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        installGameSignals();
        installLevelSignals();
        installGameCommandSignals();
        installUICommandSignals();
        installResourceSignals();
        installSpawnSignals();
        installInputSignals();
        installRecordSignals();
    }

    private void installGameSignals()
    {
        Container.DeclareSignal<GameLoadedSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameUnloadedSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameStartedSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameWonSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameLostSignal>().OptionalSubscriber();
        Container.DeclareSignal<GamePausedSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameResumedSignal>().OptionalSubscriber();
    }
    private void installLevelSignals()
    {
        Container.DeclareSignal<LevelsInitializedSignal>().OptionalSubscriber();
        Container.DeclareSignal<LevelModifiedSignal>().OptionalSubscriber();
        Container.DeclareSignal<LevelSelectedSignal>().OptionalSubscriber();
    }
    private void installGameCommandSignals()
    {
        Container.DeclareSignal<StartGameCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<PauseGameCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<ResumeGameCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<WinGameCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<LoseGameCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<UnloadGameCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<RestartGameCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<LoadNextLevelCommandSignal>().OptionalSubscriber();
    }
    private void installUICommandSignals()
    {
        Container.DeclareSignal<OpenLevelBrowserPageCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<CloseLevelBrowserPageCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<SelectLevelCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<OpenRVPageCommandSignal>().OptionalSubscriber();
        Container.DeclareSignal<CloseRVPageCommandSignal>().OptionalSubscriber();
    }

    private void installResourceSignals()
    {
        Container.DeclareSignal<ResourceChangedSignal>().OptionalSubscriber();
    }
    private void installRecordSignals()
    {
        Container.DeclareSignal<RecordChangedSignal>().OptionalSubscriber();
    }
    private void installSpawnSignals()
    {
        Container.DeclareSignal<SpawnPointsCreatedSignal>().OptionalSubscriber();
    }

    private void installInputSignals()
    {
        Container.DeclareSignal<GroundClickedSignal>().OptionalSubscriber();
    }
}