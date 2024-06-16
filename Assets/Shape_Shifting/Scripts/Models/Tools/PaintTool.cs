using UnityEngine;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Paint Tool Model", menuName = "Models/Tools/Paint Tool Model")]

    public class PaintTool : ToolModel
    {
        private void OnValidate()
        {
            ToolData.ToolType = eToolType.Paint;
        }


        public override void OnDeselect()
        {
            
        }

        public override void OnSelect()
        {

        }

        public override void OnDrag(Vector2 i_DragDelta, Vector2 i_MousePosition)
        {
            
        }

        public override void OnMouseDown(Vector2 i_MousePosition)
        {
           
        }

        public override void OnMouseUp(Vector2 i_MousePosition)
        {
            
        }
    }
}

