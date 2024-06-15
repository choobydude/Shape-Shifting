using UnityEngine;
using UnityEngine.UI;

namespace ShapeShifting
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class UIGroupBase : UIElementBase
    {
        protected override void Awake()
        {
            base.Awake();
            Hide();
        }
    }
}