using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ShapeShifting
{
    [System.Serializable]
    public class ButtonAnimationSettings
    {
        [field: SerializeField, BoxGroup("Animations")] public Ease AnimationEasing { get; private set; }
        [field: SerializeField, BoxGroup("Animations")] public float AnimationDuration { get; private set; }
        [field: SerializeField, BoxGroup("Animations"), InfoBox("Value is relative to initial scale")] public float ScaleDownAmount { get; private set; }
    }
}

