using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEditor;
using System.Linq;

namespace ShapeShifting
{
    [RequireComponent(typeof(Image))]
    public class Button : MonoBehaviour,
        IPointerDownHandler,
        IPointerClickHandler,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerUpHandler,
        IDragHandler,
        IBeginDragHandler,
        IEndDragHandler
    {
        #region Serialized Fields

        [field: SerializeField, BoxGroup("Interaction")] public bool IsInteractable { get; private set; } = true;

        #endregion

        #region Enevts
        [Space]
        [FoldoutGroup("Events")]
        public UnityEvent OnClick;
        [FoldoutGroup("Events")]
        public UnityEvent OnPressed;
        [FoldoutGroup("Events")]
        public UnityEvent OnReleased;
        [FoldoutGroup("Events")]
        public UnityEvent OnDragged;
        [FoldoutGroup("Events")]
        public UnityEvent<bool> OnInteractionChanged;
        #endregion

        #region Interaction Methods
        public virtual void SetInteractable(bool i_IsInteractable)
        {
            IsInteractable = i_IsInteractable;
            OnInteractionChanged?.Invoke(IsInteractable);
        }
        protected virtual void Press()
        {
            OnPressed?.Invoke();
        }
        protected virtual void Release()
        {
            OnReleased?.Invoke();
        }
        protected virtual void Drag()
        {
            OnDragged?.Invoke();
        }
        protected virtual void Click()
        {
            OnClick?.Invoke();
        }
        #endregion

        #region Pointer Events
        public void OnBeginDrag(PointerEventData eventData)
        {

        }
        public void OnDrag(PointerEventData eventData)
        {
            if (IsInteractable)
                Drag();
        }
        public void OnEndDrag(PointerEventData eventData)
        {

        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsInteractable)
                Click();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsInteractable)
                Press();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {

        }
        public void OnPointerExit(PointerEventData eventData)
        {

        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (IsInteractable)
                Release();
        }
        #endregion
    }
}

