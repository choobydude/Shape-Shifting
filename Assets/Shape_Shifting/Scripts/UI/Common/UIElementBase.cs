using System;
using UnityEngine;

namespace WhackAMole
{
    public abstract class UIElementBase : MonoBehaviour
    {
        #region Fields
        public bool IsVisible { get; private set; }
        #endregion

        #region Mono Methods
        protected virtual void Awake()
        {
            IsVisible = gameObject.activeInHierarchy;
        }
        #endregion

        #region Visibility Methods

        public void Show()
        {
            Show(null);
        }
        public void Hide()
        {
            Hide(null);
        }

        public virtual void Show(Action i_OnComplete)
        {
            if (IsVisible)
                return;
            
            gameObject.SetActive(true);
            IsVisible = true;

            i_OnComplete?.Invoke();
        }
        public virtual void Hide(Action i_OnComplete)
        {
            if (!IsVisible)
                return;

            gameObject.SetActive(false);
            IsVisible = false;

            i_OnComplete?.Invoke();
        }
        #endregion
    }
}

