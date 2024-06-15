using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WhackAMole
{
    [System.Serializable]
    public class ButtonSaturationSettings
    {
        [field: SerializeField, BoxGroup("Saturation")] public bool DesaturateWhenNotInteractable { get; private set; }
        [field: SerializeField, BoxGroup("Saturation")] public bool DesaturateChildren { get; private set; }
        [field: SerializeField, BoxGroup("Saturation")] public Material DesaturateMaterial { get; private set; }
        [field: SerializeField, BoxGroup("Saturation")] public Material DefaultMaterial { get; private set; }
    }
}

