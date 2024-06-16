using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class CircularLookAt2D : MonoBehaviour
    {
        [SerializeField] bool m_IsLookAtCursor;
        [SerializeField] bool m_IsLookAtPosition;
        [SerializeField, ShowIf(nameof(m_IsLookAtPosition))] Vector2 m_LookAtPosition;
        [SerializeField] Transform m_Visual;
        [SerializeField] float m_RadiusLimit;
        Camera m_Camera => Camera.main;

        private void Update()
        {
            if (m_IsLookAtCursor)
                lookAtCursor();
            else if (m_IsLookAtPosition)
                lookAt(m_LookAtPosition);
        }

        private void lookAtCursor()
        {
            Vector3 cursorPosition = Input.mousePosition;
            cursorPosition.z = -m_Camera.transform.position.z;
            lookAt(m_Camera.ScreenToWorldPoint(cursorPosition));
        }
        private void lookAt(Vector2 i_Target)
        {
            m_Visual.transform.position = (Vector2)transform.position + Vector2.ClampMagnitude(i_Target - (Vector2)transform.position, m_RadiusLimit);
        }
    }
}

