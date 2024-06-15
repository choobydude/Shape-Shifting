using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShapeShifting
{
    public class ToolView : SignalButtonBase
    {
        [SerializeField] eToolType m_ToolType;
        [SerializeField] Image m_IconImage;
        [SerializeField] Image m_ButtonImage;
        Color m_SelectedIconColor, m_DeselectedIconColor, m_SelectedButtonColor, m_DeselectedButtonColor;
        [Inject]
        ToolController m_ToolController;

        private void OnEnable()
        {
            subscribeSignals();
            manualUpdate();
        }
        private void OnDisable()
        {
            unsubscribeSignals();
        }
        private void OnDestroy()
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

        private void manualUpdate()
        {
            if (m_ToolController.GetToolData(m_ToolType, out ToolData o_ToolData))
            {
                m_SelectedIconColor = o_ToolData.SelectedColors[0];
                m_SelectedButtonColor = o_ToolData.SelectedColors[1];

                m_DeselectedIconColor = o_ToolData.DeselectedColors[0];
                m_DeselectedButtonColor = o_ToolData.DeselectedColors[1];

                m_IconImage.sprite = o_ToolData.Icon;

                updateSelectedVisual(o_ToolData.IsSelected);
            }
        }

        private void updateSelectedVisual(bool i_IsSelected)
        {
            m_IconImage.color = (i_IsSelected) ? m_SelectedIconColor : m_DeselectedIconColor;
            m_ButtonImage.color = (i_IsSelected) ? m_SelectedButtonColor : m_DeselectedButtonColor;
        }


        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire(new SelectToolCommandSignal(m_ToolType));
        }
    }
}