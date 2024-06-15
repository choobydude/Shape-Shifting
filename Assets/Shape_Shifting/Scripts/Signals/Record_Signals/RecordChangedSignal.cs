namespace ShapeShifting
{
    public class RecordChangedSignal
    {
        public eRecordType RecordType { get; private set; }
        public int Value { get; private set; }

        public RecordChangedSignal(eRecordType i_RecordType, int i_Value)
        {
            RecordType = i_RecordType;
            Value = i_Value;
        }
    }
}