using UnityEngine;

namespace WhackAMole
{
    [System.Serializable]
    public class GameControllerSettings
    {
        [field: SerializeField] public ConditionBase StartGameCondition { get; private set; }
        [field: SerializeField] public ConditionBase LoseGameCondition { get; private set; }
        [field: SerializeField] public ConditionBase WinGameCondition { get; private set; }
        [field: SerializeField] public int TargetFrameRate { get; private set; } = 60;
    }
}