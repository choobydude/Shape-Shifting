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
        }
        private void tryScalePreview()
        {
            if (!m_ErasePreview)
                return;

            float scale = m_ErasePreview.transform.localScale.x + Input.mouseScrollDelta.y * m_ScrollSensitivity * Time.deltaTime;
            scale = Mathf.Clamp(scale, m_MinScale, m_MaxScale);
            m_ErasePreview.transform.localScale = Vector3.one * scale;
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

