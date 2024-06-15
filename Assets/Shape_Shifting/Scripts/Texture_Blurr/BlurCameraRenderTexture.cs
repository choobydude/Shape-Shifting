using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShapeShifting
{
    public class BlurCameraRenderTexture : MonoBehaviour
    {
        [Inject(Id = "Effect")]
        Camera m_EffectCamera;
        [SerializeField, ReadOnly] RawImage m_RenderTextureView;
        [SerializeField]
        int m_Radius;
        [SerializeField]
        float m_Sigma;

        private void setRefs()
        {
            m_RenderTextureView = GetComponent<RawImage>();
        }

        private void OnValidate()
        {
            setRefs();
        }
      
        private void Start()
        {
            m_RenderTextureView.texture = m_EffectCamera.targetTexture;
        }
    }
}

