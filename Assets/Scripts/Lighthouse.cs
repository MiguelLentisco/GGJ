using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    LuzFaro ligth;
    Quaternion originalRot;
    [SerializeField] float maxDistance = 3f;
    float minScale = 1.6f;
    float maxScale = 2.667f;

    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;

        ligth = GameObject.FindGameObjectWithTag("Luz").GetComponent<LuzFaro>();
        ligth.ChangeDistance(maxDistance);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(ligth.transform.position.x, transform.position.y, ligth.transform.position.z));
    }

    public IEnumerator ScaleUpOverTime(float time)
    {
        Vector3 originalScale = new Vector3(minScale, minScale, minScale);
        Vector3 destinationScale = new Vector3(maxScale, maxScale, maxScale);

        float currentTime = 0.0f;

        do
        {
            gameObject.transform.Find("Limit").transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            maxDistance = 5f;
            ligth.ChangeDistance(maxDistance);
            yield return null;
        } while (currentTime <= time);

    }

    public IEnumerator ScaleDownOverTime(float time)
    {
        Vector3 originalScale = new Vector3(maxScale, maxScale, maxScale);
        Vector3 destinationScale = new Vector3(minScale, minScale, minScale);

        float currentTime = 0.0f;

        do
        {
            gameObject.transform.Find("Limit").transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            maxDistance = 3f;
            ligth.ChangeDistance(maxDistance);
            yield return null;
        } while (currentTime <= time);

    }

}
