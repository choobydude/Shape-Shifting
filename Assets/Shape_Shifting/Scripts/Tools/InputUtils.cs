using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WhackAMole
{
    public static class InputUtils
    {
        public static bool IsMouseOverUI()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results.Any(x => x.gameObject.layer == LayerMask.NameToLayer("UI"));
        }
    }
}

