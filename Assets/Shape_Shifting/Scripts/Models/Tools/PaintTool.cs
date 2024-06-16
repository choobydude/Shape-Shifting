using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    [CreateAssetMenu(fileName = "New Paint Tool Model", menuName = "Models/Tools/Paint Tool Model")]

    public class PaintTool : ToolModel
    {
        [SerializeField] Blob m_BlobPrefab;
        [SerializeField] Transform m_PreviewBlobPrefab;
        [SerializeField] float m_BlobMinScale, m_BlobMaxScale;
        [SerializeField] float m_ScrollSensitivity;
        [SerializeField] float m_DistanceThreshold;
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
            followCursor();
        }

        public override void Update()
        {
            base.Update();

            tryScalePreview();
            followCursor();
        }

        private void tryScalePreview()
        {
            if (!m_PreviewBlob)
                return;

            float scale = m_PreviewBlob.transform.localScale.x + Input.mouseScrollDelta.y * m_ScrollSensitivity * Time.deltaTime;
            scale = Mathf.Clamp(scale, m_BlobMinScale, m_BlobMaxScale);
            m_PreviewBlob.transform.localScale = Vector3.one * scale;
        }

        private void followCursor()
        {
            if (m_PreviewBlob)
                m_PreviewBlob.transform.position = MouseWorldPosition;
        }
        private void tryPaintBlob(Vector2 i_MouseWorldPosition)
        {
            if (!m_PreviewBlob)
                return;

            Blob blob = BlobGroup.GetClosestBlob(i_MouseWorldPosition, m_PreviewBlob.transform.localScale.x / 2, out float o_DistanceBetween);
            if (Vector3.Distance(MouseWorldPosition, MouseWorldPreviousPosition) >= m_DistanceThreshold)
                paintBlob(i_MouseWorldPosition);
        }

        private void paintBlob(Vector2 i_Position)
        {
            Blob blob = Instantiate(m_BlobPrefab);
            blob.transform.position = i_Position;
            blob.transform.localScale = m_PreviewBlob.localScale;

            BlobGroup.AddBlob(blob);
        }

        public override void OnMouseDown(Vector2 i_MouseWorldPosition)
        {

        }

        public override void OnMouse(Vector2 i_MouseWorldPosition)
        {
            tryPaintBlob(i_MouseWorldPosition);
        }


        public override void OnMouseUp(Vector2 i_MouseWorldPosition)
        {

        }
    }
}

