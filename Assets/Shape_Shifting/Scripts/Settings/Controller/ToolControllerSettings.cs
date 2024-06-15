using System.Collections.Generic;
using UnityEngine;

namespace ShapeShifting
{
    [System.Serializable]
    public class ToolControllerSettings
    {
        [field: SerializeField] public List<ToolModel> Tools { get; private set; }
    }
}