using System.Collections.Generic;

namespace ShapeShifting
{
    public class LevelsInitializedSignal
    {
        public List<LevelData> LevelsData { get; private set; }

        public LevelsInitializedSignal(List<LevelData> i_LevelsData)
        {
            LevelsData = i_LevelsData;
        }
    }
}

