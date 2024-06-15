namespace WhackAMole
{
    public class GameLoadedSignal
    {
        public LevelData LevelData { get; private set; }

        public GameLoadedSignal(LevelData i_LevelData)
        {
            LevelData = i_LevelData;
        }
    }
}
