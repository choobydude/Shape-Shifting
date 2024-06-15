using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class InputController : ITickable
    {

        #region Fields
        [Inject(Id = "Main")]
        Camera m_Camera;
        [Inject]
        SignalBus m_SignalBus;

        #endregion

        #region Input

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0) && !InputUtils.IsMouseOverUI())
                onPointerDown(Input.mousePosition);
        }

        private void onPointerDown(Vector3 i_PointerPosition)
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = m_Camera.ScreenPointToRay(i_PointerPosition);
            if (groundPlane.Raycast(ray, out float enter))
                fireGroundClickedSignal(ray.GetPoint(enter));
        }

        private void fireGroundClickedSignal(Vector3 i_ClickPosition)
        {
            m_SignalBus.TryFire(new GroundClickedSignal(i_ClickPosition));
        }
        #endregion
    }
}