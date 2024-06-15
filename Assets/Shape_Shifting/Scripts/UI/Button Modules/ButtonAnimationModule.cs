using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    [RequireComponent(typeof(Button))]
    public class ButtonAnimationModule : ButtonModuleBase
    {
        #region Serialized Fields

        [SerializeField] private bool m_UseGlobalSettings = true;
        [SerializeField, HideIf(nameof(m_UseGlobalSettings))] private ButtonAnimationSettings m_ExclusiveSettings;

        #endregion

        #region Non Serialized Fields
        private Vector3 m_InitialScale;
        [Inject]
        private ButtonAnimationSettings m_GlobalSettings;
        private ButtonAnimationSettings ActiveSettings
        {
            get
            {
                return (m_UseGlobalSettings) ? m_GlobalSettings : m_ExclusiveSettings;
            }
        }

        #endregion

        #region Mono Events
        protected override void Awake()
        {
            base.Awake();
            m_InitialScale = Button.transform.localScale;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            stopAnimation();
            resetScale();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            stopAnimation();
        }


        #endregion

        #region Event Handling
        protected override void subscribeButtonEvents()
        {
            Button.OnPressed.AddListener(onButtonPressed);
            Button.OnReleased.AddListener(onButtonReleased);
        }
        protected override void unsubscribeButtonEvents()
        {
            Button.OnPressed.RemoveListener(onButtonPressed);
            Button.OnReleased.RemoveListener(onButtonReleased);
        }
        private void onButtonPressed()
        {
            playPressAnimation();
        }
        private void onButtonReleased()
        {
            playReleaseAnimation();
        }
        #endregion

        #region Animation
        private void resetScale()
        {
            Button.transform.localScale = m_InitialScale;
        }
        private void stopAnimation()
        {
            DOTween.Kill(Button.transform);
        }
        private void playPressAnimation()
        {
            stopAnimation();
            transform.DOScale(m_InitialScale * ActiveSettings.ScaleDownAmount, ActiveSettings.AnimationDuration).SetEase(ActiveSettings.AnimationEasing);
        }
        private void playReleaseAnimation()
        {
            stopAnimation();
            transform.DOScale(m_InitialScale, ActiveSettings.AnimationDuration).SetEase(ActiveSettings.AnimationEasing);
        }
        #endregion
    }
}

