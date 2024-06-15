using UnityEngine;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Erase Tool Model", menuName = "Models/Tools/Erase Tool Model")]

    public class EraseTool : ToolModel
    {
        private void OnValidate()
        {
            ToolType = eToolType.Erase;
        }

        public override void OnDeselect()
        {
            
        }

        public override void OnSelect()
        {
            
        }

        public override void OnUpdate()
        {
            
        }
    }
}

