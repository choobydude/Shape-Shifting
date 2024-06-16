using Sirenix.OdinInspector;
using UnityEngine;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Grab Tool Model", menuName = "Models/Tools/Grab Tool Model")]

    public class GrabTool : ToolModel
    {
        [SerializeField] private float m_DragSensitivity;

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


        BlobGroup m_BlobGroup;
        Vector2 m_BlobGroupStartPosition;

        public override void OnMouseDown(Vector2 i_MouseWorldPosition)
        {
            if (GetBlobUnderCursor(out Blob o_Blob))
            {

                m_BlobGroup = o_Blob.GetComponentInParent<BlobGroup>();
                m_BlobGroupStartPosition = m_BlobGroup.transform.position;
            }
        }
        public override void OnDrag(Vector2 i_DragDeltaWorld, Vector2 i_MouseWorldPosition)
        {
            if (m_BlobGroup)
            {
                Vector2 offset = i_MouseWorldPosition - MouseWorldStartposition;
                m_BlobGroup.transform.position = m_BlobGroupStartPosition + offset * m_DragSensitivity;
            }
        }
        public override void OnMouseUp(Vector2 i_MouseWorldPosition)
        {

            m_BlobGroup = null;
        }
    }
}

