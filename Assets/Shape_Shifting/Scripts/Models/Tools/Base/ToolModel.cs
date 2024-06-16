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
        protected Camera Camera;
        [Inject]
        protected BlobGroup BlobGroup;

        private void fireSelectedEvent()
        {
            SignalBus.TryFire(new ToolSelectedSignal(ToolData.ToolType));
        }

        public void Select()
        {
            Cursor.SetCursor(ToolData.NormalCursonTexture, Vector2.zero, CursorMode.Auto);

            ToolData.IsSelected = true;
            fireSelectedEvent();
            OnSelect();
        }

        protected Vector2 MouseWorldStartPosition;
        protected Vector2 MouseWorldPreviousPosition;
        protected Vector2 MouseWorldPosition;

        public virtual void Update()
        {
            MouseWorldPreviousPosition = MouseWorldPosition;
            MouseWorldPosition = Camera.ScreenToWorldPoint((Vector2)Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(ToolData.PressedCursonTexture, Vector2.zero, CursorMode.Auto);

                MouseWorldStartPosition = MouseWorldPosition;
                OnMouseDown(MouseWorldStartPosition);
            }
            else if (Input.GetMouseButton(0))
            {
                OnMouse(MouseWorldPosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Cursor.SetCursor(ToolData.NormalCursonTexture, Vector2.zero, CursorMode.Auto);

                OnMouseUp(MouseWorldPosition);
            }
        }
        public void Deselect()
        {
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


        public abstract void OnMouse(Vector2 i_MouseWorldPosition);
        public abstract void OnMouseDown(Vector2 i_MouseWorldPosition);
        public abstract void OnMouseUp(Vector2 i_MouseWorldPosition);

    }
    [System.Serializable]
    public struct ToolData
    {
        public bool IsSelected;
        public eToolType ToolType;
        [PreviewField] public Sprite Icon;
        [PreviewField] public Texture2D NormalCursonTexture;
        [PreviewField] public Texture2D PressedCursonTexture;
        public Color[] SelectedColors;
        public Color[] DeselectedColors;
    }
}

