using Sirenix.OdinInspector;
using UnityEngine;

namespace ShapeShifting
{
    [ExecuteInEditMode]
    public class SafeAreaFitter : MonoBehaviour
    {
        [SerializeField, ReadOnly, Required("Rect Transform is missing")]
        RectTransform m_ThisRectTransform;
        [SerializeField, ReadOnly, Required("Canvas is missing, Make sure object is under canvas hierarchy")]
        Canvas m_ParentCanvas;
        Rect m_SafeArea;

        private void Start()
        {
            fittInSafeArea();
        }

#if UNITY_EDITOR
        private void Update()
        {
            fittInSafeArea();
        }

        private void OnValidate()
        {
            setRefs();
        }
#endif

        private void setRefs()
        {
            m_ThisRectTransform = GetComponent<RectTransform>();
            m_ParentCanvas = GetComponentInParent<Canvas>();
        }


        private void fittInSafeArea()
        {
            if (m_ParentCanvas == null)
                m_ParentCanvas = GetComponentInParent<Canvas>();

            if (m_ParentCanvas == null)
                return;

            if (m_ThisRectTransform == null)
            {
                m_ThisRectTransform = GetComponent<RectTransform>();
                if (m_ThisRectTransform == null)
                    m_ThisRectTransform = gameObject.AddComponent<RectTransform>();
            }

            if (m_ThisRectTransform == null)
                return;


            m_SafeArea = Screen.safeArea;

            m_ThisRectTransform.anchorMin = Vector2.zero;
            m_ThisRectTransform.anchorMax = Vector2.zero;

            m_ThisRectTransform.sizeDelta = m_SafeArea.size / m_ParentCanvas.scaleFactor;
            m_ThisRectTransform.anchoredPosition = m_SafeArea.center / m_ParentCanvas.scaleFactor;
        }
    }
}
