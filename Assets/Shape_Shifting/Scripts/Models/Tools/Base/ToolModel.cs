using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public abstract class ToolModel : ScriptableObject
    {
        [SerializeField] public ToolData ToolData;

        [Inject]
        protected SignalBus SignalBus;

        private void fireSelectedEvent()
        {
            SignalBus.TryFire(new ToolSelectedSignal(ToolData.ToolType));
        }

        public void Select()
        {
            if (ToolData.IsSelected)
                return;
            ToolData.IsSelected = true;
            fireSelectedEvent();
            OnSelect();
        }
        public void Update()
        {
            OnUpdate();
        }
        public void Deselect()
        {
            if (!ToolData.IsSelected)
                return;
            ToolData.IsSelected = false;
            OnDeselect();
        }


        public abstract void OnSelect();

        public abstract void OnUpdate();

        public abstract void OnDeselect();

    }
    [System.Serializable]
    public struct ToolData
    {
        public bool IsSelected;
        public eToolType ToolType;
        public Sprite Icon;
        public Sprite CursonIcon;
        public Color[] SelectedColors;
        public Color[] DeselectedColors;
    }
}

