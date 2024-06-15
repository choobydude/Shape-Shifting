namespace ShapeShifting
{
    public class SelectToolCommandSignal 
    {
        public eToolType ToolType { get; private set; }

        public SelectToolCommandSignal(eToolType i_ToolType)
        {
            ToolType = i_ToolType;
        }
    }
}