namespace ShapeShifting
{
    public class SelectLevelCommandSignal
    {
        public string LevelName { get; private set; }

        public SelectLevelCommandSignal(string i_LevelName)
        {
            LevelName = i_LevelName;
        }
    }
}