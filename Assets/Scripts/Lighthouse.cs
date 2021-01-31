using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    LuzFaro luzFaro = null;
    Transform limit = null;
    Transform face = null;

    // Start is called before the first frame update
    void Start()
    {
        face = transform.Find("faro_01").Find("luz_faro");
        luzFaro = GameObject.FindGameObjectWithTag("Luz").GetComponent<LuzFaro>();
        limit = transform.Find("Limit");
    }

    // Update is called once per frame
    void Update()
    {
        face.LookAt(new Vector3(luzFaro.transform.position.x, transform.position.y, luzFaro.transform.position.z));
    }

    public IEnumerator ScaleOverTime(float scalePercent, float time)
    {
        Vector3 originalScale = limit.localScale; 
        float currentTime = 0.0f;
        
        while (currentTime <= time)
        {
            limit.localScale = Vector3.Lerp(originalScale, originalScale * scalePercent, currentTime / time);
            luzFaro.UpdateMaxDistance();
            yield return null;
            currentTime += Time.deltaTime;
        }
    }
}
