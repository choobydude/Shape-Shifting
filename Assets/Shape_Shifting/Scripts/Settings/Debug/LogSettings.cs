using UnityEngine;

namespace WhackAMole
{
    [System.Serializable]
    public class LogSettings
    {
        [field: SerializeField] public bool IsLogEnabled { get; private set; }
    }
}

