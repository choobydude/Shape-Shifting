using System;
using UnityEngine;

namespace ShapeShifting
{
    public abstract class UIElementBase : MonoBehaviour
    {
        #region Fields
        public bool IsVisible { get; private set; } = false;
        #endregion

        #region Mono Methods
        protected virtual void Awake()
        {

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

