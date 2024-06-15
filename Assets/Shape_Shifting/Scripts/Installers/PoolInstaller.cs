using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private LevelView m_LevelViewPrefab;
        [SerializeField] private SpawnPoint m_SpawnPointPrefab;
        [SerializeField] private SpawnObject m_SpawnPrefab;

        public override void InstallBindings()
        {
            installLevelViewPool();
            installSpawnPointPool();
            installSpawnObjectBasePool();
        }

        private void installLevelViewPool()
        {
            Container.BindMemoryPool<LevelView, LevelView.Pool>()
            .WithInitialSize(20)
            .ExpandByOneAtATime()
            .FromComponentInNewPrefab(m_LevelViewPrefab)
            .UnderTransform(transform);
        }
        private void installSpawnPointPool()
        {
            Container.BindMemoryPool<SpawnPoint, SpawnPoint.Pool>()
           .WithInitialSize(20)
           .ExpandByOneAtATime()
           .FromComponentInNewPrefab(m_SpawnPointPrefab)
           .UnderTransform(transform);
        }

        private void installSpawnObjectBasePool()
        {
            Container.BindMemoryPool<SpawnObject, SpawnObject.Pool>()
           .WithInitialSize(20)
           .ExpandByOneAtATime()
           .FromComponentInNewPrefab(m_SpawnPrefab)
           .UnderTransform(transform);
        }
    }
}
