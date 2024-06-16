using UnityEngine;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Erase Tool Model", menuName = "Models/Tools/Erase Tool Model")]

    public class EraseTool : ToolModel
    {
        [SerializeField] Transform m_ErasePreviewPrefab;
        Transform m_ErasePreview;
        [SerializeField] float m_MinScale, m_MaxScale;
        [SerializeField] float m_ScrollSensitivity;

        private void OnValidate()
        {
            ToolData.ToolType = eToolType.Erase;
        }

        public override void OnDeselect()
        {
            if (m_ErasePreview)
                m_ErasePreview.gameObject.SetActive(false);
        }

        public override void OnSelect()
        {
            if (!m_ErasePreview)
                m_ErasePreview = Instantiate(m_ErasePreviewPrefab);

            m_ErasePreview.gameObject.SetActive(true);
            followCursor();
        }
        public override void Update()
        {
            base.Update();
            tryScalePreview();
            followCursor();
        }

        private void followCursor()
        {
            if (m_ErasePreview)
                m_ErasePreview.transform.position = MouseWorldPosition;
        }

        private void tryScalePreview()
        {
            if (!m_ErasePreview)
                return;

            float scale = m_ErasePreview.transform.localScale.x + Input.mouseScrollDelta.y * m_ScrollSensitivity * Time.deltaTime;
            scale = Mathf.Clamp(scale, m_MinScale, m_MaxScale);
            m_ErasePreview.transform.localScale = Vector3.one * scale;
        }

        private void tryErase(Vector2 i_MouseWorldPosition)
        {
            if (!m_ErasePreview)
                return;

            BlobGroup.EraseBlobsInRadius(i_MouseWorldPosition, m_ErasePreview.localScale.x / 2);
        }

        public override void OnMouseDown(Vector2 i_MouseWorldPosition)
        {

        }

        public override void OnMouse(Vector2 i_MouseWorldPosition)
        {
            tryErase(i_MouseWorldPosition);
        }


        public override void OnMouseUp(Vector2 i_MouseWorldPosition)
        {

        }
    }
}

