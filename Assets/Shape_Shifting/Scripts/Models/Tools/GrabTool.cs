using UnityEngine;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Grab Tool Model", menuName = "Models/Tools/Grab Tool Model")]

    public class GrabTool : ToolModel
    {
        private void OnValidate()
        {
            ToolData.ToolType = eToolType.Grab;
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

