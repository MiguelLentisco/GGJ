using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    LuzFaro luzFaro;
    Transform limit;
    float originalScale;
    Transform face;

    float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        face = transform.Find("faro_01").Find("luz_faro");
        luzFaro = GameObject.FindGameObjectWithTag("Luz").GetComponent<LuzFaro>();
        limit = transform.Find("Limit");
        originalScale = limit.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        face.LookAt(new Vector3(luzFaro.transform.position.x, transform.position.y, luzFaro.transform.position.z));
    }

    public IEnumerator ScaleUpOverTime(float maxScale, float time)
    {
        this.maxScale = maxScale;
        Vector3 destinationScale = new Vector3(maxScale, maxScale, maxScale);

        float currentTime = 0.0f;

        do
        {
            gameObject.transform.Find("Limit").transform.localScale = Vector3.Lerp(limit.localScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            luzFaro.UpdateMaxDistance();
            yield return null;
        } while (currentTime <= time);

    }

    public IEnumerator ScaleDownOverTime(float time)
    {
        Vector3 originalScale = new Vector3(maxScale, maxScale, maxScale);
        Vector3 destinationScale = new Vector3(this.originalScale, this.originalScale, this.originalScale);

        float currentTime = 0.0f;
        do
        {
            gameObject.transform.Find("Limit").transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            luzFaro.UpdateMaxDistance();
            yield return null;
        } while (currentTime <= time);

    }

}
