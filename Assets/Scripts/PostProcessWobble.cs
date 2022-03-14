using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessWobble : MonoBehaviour
{
    [SerializeField] PostProcessVolume ppVolume;
    [SerializeField] float minValueDistortion = -0.5f;
    [SerializeField] float maxValueDistortion = 0.5f;
    [SerializeField] float cycleDurationDistortion = 0.5f;
    [SerializeField] float minValueGrain = 0.5f;
    [SerializeField] float maxValueGrain = 1.0f;
    [SerializeField] float cycleDurationGrain = 0.25f;

    LensDistortion distortion;
    Grain grain;

    private void Start()
    {
        if (ppVolume.profile.TryGetSettings<LensDistortion>(out LensDistortion outDistortion))
        {
            distortion = outDistortion;
        }

        if (ppVolume.profile.TryGetSettings<Grain>(out Grain outGrain))
        {
            grain = outGrain;
        }
    }

    void Update()
    {
        if (distortion != null)
        {
            float t = (Mathf.Sin(Time.time / cycleDurationDistortion) + 1) * 0.5f;
            distortion.centerX.value = Mathf.Lerp(minValueDistortion, maxValueDistortion, t);
        }

        if (grain != null)
        {
            float t = (Mathf.Sin(Time.time / cycleDurationGrain) + 1) * 0.5f;
            grain.intensity.value = Mathf.Lerp(minValueGrain, maxValueGrain, t);
        }
    }
}
