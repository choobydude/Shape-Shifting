namespace WhackAMole
{
    public class ResourceChangedSignal 
    {
        public eResourceType ResourceType { get; private set; }
        public int Value { get; private set; }

        public ResourceChangedSignal(eResourceType i_ResourceType, int i_Value)
        {
            ResourceType = i_ResourceType;
            Value = i_Value;
        }
    }
}

