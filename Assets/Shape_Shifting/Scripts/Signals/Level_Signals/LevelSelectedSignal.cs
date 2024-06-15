namespace ShapeShifting
{
    public class LevelSelectedSignal
    {
        public LevelData LevelData { get; private set; }

        public LevelSelectedSignal(LevelData i_Data)
        {
            LevelData = i_Data;
        }
    }
}