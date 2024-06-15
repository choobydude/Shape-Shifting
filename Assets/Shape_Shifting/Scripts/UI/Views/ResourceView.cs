using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShapeShifting
{
    public class ResourceView : UIElementBase
    {
        #region Fields
        [SerializeField] private eResourceType m_ResourceType;
        [SerializeField] private Image m_IconImage;
        [SerializeField] private TextMeshProUGUI m_ValueText;
        [Inject]
        SignalBus m_SignalBus;
        [Inject]
        ResourceController m_ResourceController;
        #endregion

        #region Mono Methods
        private void OnEnable()
        {
            manualUpdate();
            subscribeSignals();
        }
        private void OnDisable()
        {
            unsubscribeSignals();
        }
        private void OnDestroy()
        {
            unsubscribeSignals();
        }

        #endregion

        #region Signal Handling
        private void subscribeSignals()
        {
            m_SignalBus.Subscribe<ResourceChangedSignal>(onResourceChanged);
        }
        private void unsubscribeSignals()
        {
            m_SignalBus.TryUnsubscribe<ResourceChangedSignal>(onResourceChanged);
        }
        private void onResourceChanged(ResourceChangedSignal i_Signal)
        {
            if (m_ResourceType != i_Signal.ResourceType)
                return;
            setText(i_Signal.Value);
        }
        #endregion

        #region Visual Update Methods
        private void manualUpdate()
        {
            if (m_ResourceController.GetResourceIconAndValue(m_ResourceType, out Sprite o_Icon, out int o_Value))
            {
                setIcon(o_Icon);
                setText(o_Value);
            }
        }
        private void setText(int i_Value)
        {
            m_ValueText.text = i_Value.ToString();
        }
        private void setIcon(Sprite i_IconSprite)
        {
            m_IconImage.sprite = i_IconSprite;
        }
        #endregion
    }
}

