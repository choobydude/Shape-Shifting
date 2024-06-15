using System.Linq;
using Zenject;

namespace WhackAMole
{
    public class LoggerInstaller : MonoInstaller<LoggerInstaller>
    {
        [Inject]
        LogSettings m_Settings;

        public override void InstallBindings()
        {
            if (m_Settings.IsLogEnabled)
            {
                Container.Bind(new[] { typeof(LoggerBase) }.Concat(typeof(GameEventsLogger).GetInterfaces())).To<GameEventsLogger>().AsSingle().NonLazy();
            }
        }
    }
}

