using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/GaussianBlur", typeof(UniversalRenderPipeline))]
public class GaussianBlurPostProcessingEffect : VolumeComponent, IPostProcessComponent
{
    public FloatParameter Radius = new FloatParameter(1);
    public FloatParameter Sigma = new FloatParameter(1);

    public bool IsActive() => true;
    public bool IsTileCompatible() => true;
}
