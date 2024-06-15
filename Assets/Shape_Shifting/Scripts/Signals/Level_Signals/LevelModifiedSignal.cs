namespace WhackAMole
{
    public class LevelModifiedSignal
    {
        public LevelData LevelData { get; private set; }

        public LevelModifiedSignal(LevelData i_Data)
        {
            LevelData = i_Data;
        }
    }
}