using UnityEngine;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Erase Tool Model", menuName = "Models/Tools/Erase Tool Model")]

    public class EraseTool : ToolModel
    {
        private void OnValidate()
        {
            ToolData.ToolType = eToolType.Erase;
        }

        public override void OnDeselect()
        {
            
        }

        public override void OnSelect()
        {
            
        }


        public override void OnMouseDown(Vector2 i_MouseWorldPosition)
        {

        }

        public override void OnMouse(Vector2 i_MouseWorldPosition)
        {

        }


        public override void OnMouseUp(Vector2 i_MouseWorldPosition)
        {

        }
    }
}

