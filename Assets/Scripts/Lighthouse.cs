using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    [SerializeField] Transform lightPos;
    Quaternion originalRot;
    [SerializeField] float maxDistance = 3f;
    float minScale = 1.6f;
    float maxScale = 2.667f;

    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;
        
        lightPos.gameObject.GetComponent<Luz_Faro>().ChangeDistance(maxDistance);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(lightPos.position.x, transform.position.y, lightPos.position.z));
    }

    void IncreaseRadius()
    {
        gameObject.transform.Find("Limit").transform.localScale = new Vector3(2.6667f, 2.6667f, 2.6667f);

        maxDistance = 5f;
        lightPos.gameObject.GetComponent<Luz_Faro>().ChangeDistance(maxDistance);
    }

    IEnumerator ScaleUpOverTime(float time)
    {
        Vector3 originalScale = new Vector3(minScale, minScale, minScale);
        Vector3 destinationScale = new Vector3(maxScale, maxScale, maxScale);

        float currentTime = 0.0f;

        do
        {
            gameObject.transform.Find("Limit").transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            maxDistance = 5f;
            lightPos.gameObject.GetComponent<Luz_Faro>().ChangeDistance(maxDistance);
            yield return null;
        } while (currentTime <= time);

    }

    IEnumerator ScaleDownOverTime(float time)
    {
        Vector3 originalScale = new Vector3(maxScale, maxScale, maxScale);
        Vector3 destinationScale = new Vector3(minScale, minScale, minScale);

        float currentTime = 0.0f;

        do
        {
            gameObject.transform.Find("Limit").transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            maxDistance = 3f;
            lightPos.gameObject.GetComponent<Luz_Faro>().ChangeDistance(maxDistance);
            yield return null;
        } while (currentTime <= time);

    }

}
