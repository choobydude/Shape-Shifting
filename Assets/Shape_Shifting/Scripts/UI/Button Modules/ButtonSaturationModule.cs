using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    [RequireComponent(typeof(Button))]
    public class ButtonSaturationModule : ButtonModuleBase
    {
        #region Serialized Fields

        [SerializeField] private bool m_UseGlobalSettings = true;
        [SerializeField, HideIf(nameof(m_UseGlobalSettings))] private ButtonSaturationSettings m_ExclusiveSettings;
        [SerializeField, ReadOnly, FoldoutGroup("Dependencies")] private UnityEngine.UI.Image m_TargetImage;
        [SerializeField, ReadOnly, FoldoutGroup("Dependencies")] private UnityEngine.UI.Image[] m_ChildImages;

        #endregion

        #region Non Serialized Fields

        [Inject]
        private ButtonSaturationSettings m_GlobalSettings;
        private ButtonSaturationSettings ActiveSettings
        {
            get
            {
                return (m_UseGlobalSettings) ? m_GlobalSettings : m_ExclusiveSettings;
            }
        }

        #endregion

        #region Event Handling

        protected override void subscribeButtonEvents()
        {
            Button.OnInteractionChanged.AddListener(onButtonInteractionChanged);
        }

        protected override void unsubscribeButtonEvents()
        {
            Button.OnInteractionChanged.RemoveListener(onButtonInteractionChanged);
        }

        private void onButtonInteractionChanged(bool i_IsInteractable)
        {
            if (i_IsInteractable) saturate(); else desaturate();
        }

        #endregion

        #region Saturation

        private void desaturate()
        {
            setMaterial(ActiveSettings.DesaturateMaterial);
        }
        private void saturate()
        {
            setMaterial(ActiveSettings.DefaultMaterial);
        }
        private void setMaterial(Material i_Material)
        {
            m_TargetImage.material = i_Material;
            if (ActiveSettings.DesaturateChildren)
                foreach (UnityEngine.UI.Image childImage in m_ChildImages)
                    childImage.material = i_Material;
        }

        #endregion

        #region Reference Handling

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();
            m_TargetImage = GetComponent<UnityEngine.UI.Image>();
            findChildImages();
        }

        private void findChildImages()
        {
            List<UnityEngine.UI.Image> childImages = GetComponentsInChildren<UnityEngine.UI.Image>(true).ToList();
            if (childImages.Contains(m_TargetImage))
                childImages.Remove(m_TargetImage);
            m_ChildImages = childImages.ToArray();
        }

        #endregion
    }
}

