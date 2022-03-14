using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessWobble : MonoBehaviour
{
    [SerializeField] PostProcessVolume ppVolume;
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    [SerializeField] float cycleDuration;

    LensDistortion distortion;

    private void Start()
    {
        if (ppVolume.profile.TryGetSettings<LensDistortion>(out LensDistortion outDistortion))
        {
            distortion = outDistortion;
        }
    }

    void Update()
    {
        if (distortion == null)
        {
            return;
        }
        
        float t = (Mathf.Sin(Time.time / cycleDuration) + 1) * 0.5f;
        distortion.centerX.value = Mathf.Lerp(minValue, maxValue, t);
    }
}
