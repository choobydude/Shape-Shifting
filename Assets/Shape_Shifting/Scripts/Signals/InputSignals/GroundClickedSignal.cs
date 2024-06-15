using UnityEngine;

namespace ShapeShifting
{
    public class GroundClickedSignal
    {
        public Vector3 ClickPosition { get; private set; }

        public GroundClickedSignal(Vector3 i_Position)
        {
            ClickPosition = i_Position;
        }
    }
}