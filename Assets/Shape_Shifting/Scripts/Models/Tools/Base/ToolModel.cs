using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public abstract class ToolModel : ScriptableObject
    {
        [SerializeField] public ToolData ToolData;

        [Inject]
        protected SignalBus SignalBus;
        [Inject(Id = "Main")]
        Camera m_Camera;

        private void fireSelectedEvent()
        {
            SignalBus.TryFire(new ToolSelectedSignal(ToolData.ToolType));
        }

        public void Select()
        {
            if (ToolData.IsSelected)
                return;

            Cursor.SetCursor(ToolData.NormalCursonTexture, Vector2.zero, CursorMode.Auto);

            ToolData.IsSelected = true;
            fireSelectedEvent();
            OnSelect();
        }

        protected Vector2 MouseWorldStartposition;
        protected Vector2 MouseWorldPreviousPosition;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(ToolData.PressedCursonTexture, Vector2.zero, CursorMode.Auto);

                MouseWorldStartposition = m_Camera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                OnMouseDown(MouseWorldStartposition);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 worldPos = m_Camera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                OnDrag(worldPos - MouseWorldPreviousPosition, worldPos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Cursor.SetCursor(ToolData.NormalCursonTexture, Vector2.zero, CursorMode.Auto);

                Vector2 worldPos = m_Camera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                OnMouseUp(worldPos);
            }
        }
        public void Deselect()
        {
            if (!ToolData.IsSelected)
                return;

            Cursor.SetCursor(null,Vector2.zero, CursorMode.Auto);

            ToolData.IsSelected = false;
            OnDeselect();
        }

        protected bool GetBlobUnderCursor(out Blob i_Blob)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                Blob blob = hit.collider.GetComponent<Blob>();
                if (blob)
                {
                    i_Blob = blob;
                    return true;
                }
            }
            i_Blob = default;
            return false;
        }


        public abstract void OnSelect();
        public abstract void OnDeselect();


        public abstract void OnDrag(Vector2 i_DragDelta, Vector2 i_MousePosition);
        public abstract void OnMouseDown(Vector2 i_MousePosition);
        public abstract void OnMouseUp(Vector2 i_MousePosition);

    }
    [System.Serializable]
    public struct ToolData
    {
        public bool IsSelected;
        public float ToolRadius;
        public eToolType ToolType;
        [PreviewField] public Sprite Icon;
        [PreviewField] public Texture2D NormalCursonTexture;
        [PreviewField] public Texture2D PressedCursonTexture;
        public Color[] SelectedColors;
        public Color[] DeselectedColors;
    }
}

