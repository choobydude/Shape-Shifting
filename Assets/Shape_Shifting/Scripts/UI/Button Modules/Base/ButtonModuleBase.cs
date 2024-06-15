using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonModuleBase : MonoBehaviour
    {
        #region Fields
        [SerializeField, FoldoutGroup("Dependencies"), ReadOnly] protected Button Button;
        #endregion

        #region Mono Events
        protected virtual void Awake()
        {

        }
        protected virtual void OnEnable()
        {
            subscribeButtonEvents();
        }
        protected virtual void OnDisable()
        {
            unsubscribeButtonEvents();
        }
        protected virtual void OnDestroy()
        {
            unsubscribeButtonEvents();
        }
        private void OnValidate()
        {
            ResolveDependencies();
        }
        #endregion

        #region Event Handling
        protected abstract void subscribeButtonEvents();
        protected abstract void unsubscribeButtonEvents();
        #endregion

        #region Reference Handling
        [Button, FoldoutGroup("Dependencies")]
        protected virtual void ResolveDependencies()
        {
            Button = GetComponent<Button>();
        }

        #endregion
    }
}
