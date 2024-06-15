using UnityEngine;
using UnityEngine.UI;

namespace WhackAMole
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