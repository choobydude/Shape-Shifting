using UnityEngine;
using UnityEngine.UI;

namespace ShapeShifting
{
    public class ToolView : SignalButtonBase
    {
        [SerializeField] Image m_Image;
        eToolType m_ToolType;
        [SerializeField] GameObject m_SelectedVisual;
        [SerializeField] GameObject m_DeselectedVisual;

        public void Setup(eToolType i_ToolType, Sprite i_Icon)
        {
            m_ToolType = i_ToolType;
            m_Image.sprite = i_Icon;
        }

        private void OnEnable()
        {
            subscribeSignals();
        }
        private void OnDisable()
        {
            unsubscribeSignals();
        }

        private void subscribeSignals()
        {
            SignalBus.Subscribe<ToolSelectedSignal>(onToolSelected);
        }
        private void unsubscribeSignals()
        {
            SignalBus.TryUnsubscribe<ToolSelectedSignal>(onToolSelected);
        }

        private void onToolSelected(ToolSelectedSignal i_Signal)
        {
            updateSelectedVisual(i_Signal.ToolType == m_ToolType);
        }


        private void updateSelectedVisual(bool i_IsSelected)
        {
            m_SelectedVisual.SetActive(i_IsSelected);
            m_SelectedVisual.SetActive(!i_IsSelected);
        }


        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire(new SelectToolCommandSignal(m_ToolType));
        }
    }
}