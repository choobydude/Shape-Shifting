using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShapeShifting
{
    public class ToolView : SignalButtonBase
    {
        [SerializeField] Image m_IconImage;
        [SerializeField] Image m_ButtonImage;

        eToolType m_ToolType;
        Color m_SelectedIconColor, m_DeselectedIconColor, m_SelectedButtonColor, m_DeselectedButtonColor;

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
            m_IconImage.color = (i_IsSelected) ? m_SelectedIconColor : m_DeselectedIconColor;
            m_ButtonImage.color = (i_IsSelected) ? m_SelectedButtonColor : m_DeselectedButtonColor;
        }


        protected override void Click()
        {
            base.Click();
            SignalBus.TryFire(new SelectToolCommandSignal(m_ToolType));
        }

        #region Pooling



        private void onSpawn(
            eToolType i_ToolType, 
            Sprite i_Icon,
            Color[] i_SelectedColors,
            Color[] i_DeselectedColors)
        {
            m_ToolType = i_ToolType;
            m_IconImage.sprite = i_Icon;
            m_SelectedIconColor = i_SelectedColors[0];
            m_DeselectedIconColor = i_SelectedColors[1];
            m_SelectedButtonColor = i_DeselectedColors[0];
            m_DeselectedButtonColor = i_DeselectedColors[1];

            subscribeSignals();
        }
        private void onDespawn()
        {
            unsubscribeSignals();
        }



        public class Pool : MonoMemoryPool<eToolType, Sprite, Color[], Color[],ToolView>
        {
            protected override void Reinitialize(
            eToolType i_ToolType,
            Sprite i_Icon,
            Color[] i_SelectedColors,
            Color[] i_DeselectedColors,
            ToolView i_Object)
            {
                base.Reinitialize(
                    i_ToolType,
                    i_Icon,
                    i_SelectedColors,
                    i_DeselectedColors,
                    i_Object);

                i_Object.onSpawn(
                    i_ToolType,
                    i_Icon,
                    i_SelectedColors,
                    i_DeselectedColors);
            }
            protected override void OnDespawned(ToolView i_Object)
            {
                base.OnDespawned(i_Object);
                i_Object.onDespawn();
            }
        }

        #endregion
    }
}