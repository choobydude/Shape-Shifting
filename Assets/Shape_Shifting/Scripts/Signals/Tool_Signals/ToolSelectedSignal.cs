namespace ShapeShifting
{
    public class ToolSelectedSignal
    {
        public eToolType ToolType { get; private set; }

        public ToolSelectedSignal(eToolType i_ToolType)
        {
            ToolType = i_ToolType;
        }
    }
}

