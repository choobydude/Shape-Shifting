using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public abstract class ToolModel : ScriptableObject
    {
        [field: SerializeField] public bool IsSelected { get; private set; }
        [field: SerializeField, ReadOnly] public eToolType ToolType { get; protected set; }
        [field: SerializeField, PreviewField] public Sprite Icon { get; private set; }
        [field: SerializeField, PreviewField] public Sprite CursonIcon { get; private set; }
        [Inject]
        protected SignalBus SignalBus;

        private void fireSelectedEvent()
        {
            SignalBus.TryFire(new ToolSelectedSignal(ToolType));
        }

        public void Select()
        {
            IsSelected = true;
            fireSelectedEvent();
            OnSelect();
        }
        public void Update()
        {
            OnUpdate();
        }
        public void Deselect()
        {
            IsSelected = false;
            OnDeselect();
        }


        public abstract void OnSelect();

        public abstract void OnUpdate();

        public abstract void OnDeselect();

    }
}

