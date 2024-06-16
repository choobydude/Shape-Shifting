using UnityEngine;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Paint Tool Model", menuName = "Models/Tools/Paint Tool Model")]

    public class PaintTool : ToolModel
    {
        [SerializeField] Blob m_BlobPrefab;
        [SerializeField] Transform m_PreviewBlobPrefab;
        [SerializeField] float m_BlobMinScale, m_BlobMaxScale;
        [SerializeField] float m_ScrollSensitivity;

        Transform m_PreviewBlob;

        private void OnValidate()
        {
            ToolData.ToolType = eToolType.Paint;
        }


        public override void OnDeselect()
        {
            if (m_PreviewBlob)
                m_PreviewBlob.gameObject.SetActive(false);
        }

        public override void OnSelect()
        {
            if (!m_PreviewBlob)
                m_PreviewBlob = Instantiate(m_PreviewBlobPrefab);
            
            m_PreviewBlob.gameObject.SetActive(true);
        }

        public override void Update()
        {
            base.Update();

            tryScalePreview();
            followCursor(MouseWorldPosition);
        }

        private void tryScalePreview()
        {
            if (!m_PreviewBlob)
                return;

            float scale = m_PreviewBlob.transform.localScale.x + Input.mouseScrollDelta.y * m_ScrollSensitivity * Time.deltaTime;
            scale = Mathf.Clamp(scale, m_BlobMinScale, m_BlobMaxScale);
            m_PreviewBlob.transform.localScale = Vector3.one * scale;
        }

        private void followCursor(Vector2 i_MouseWorldPosition)
        {
            if (m_PreviewBlob)
                m_PreviewBlob.transform.position = i_MouseWorldPosition;
        }
        private void tryPaintBlob(Vector2 i_MouseWorldPosition)
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

