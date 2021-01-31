using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    // Start is called before the first frame update
    Light luz = null;

    void Start()
    {
        luz = GetComponent<Light>();
    }

    public IEnumerator changeIntensity(float intensityPercent, float time)
    {
        float originalIntensity = luz.intensity;
        float currentTime = 0.0f;

        while (currentTime <= time)
        {
            luz.intensity = Mathf.Lerp(originalIntensity, originalIntensity * intensityPercent, currentTime / time);
            yield return null;
            currentTime += Time.deltaTime;
        }
    }
}
