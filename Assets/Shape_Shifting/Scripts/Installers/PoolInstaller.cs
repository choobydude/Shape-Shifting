using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private LevelView m_LevelViewPrefab;

        public override void InstallBindings()
        {
            installLevelViewPool();
        }

        private void installLevelViewPool()
        {
            Container.BindMemoryPool<LevelView, LevelView.Pool>()
            .WithInitialSize(20)
            .ExpandByOneAtATime()
            .FromComponentInNewPrefab(m_LevelViewPrefab)
            .UnderTransform(transform);
        }
    }
}
