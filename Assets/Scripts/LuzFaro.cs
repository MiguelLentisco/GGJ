using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzFaro : MonoBehaviour
{
    Light luz;
    Vector3 newPosition;
    Vector3 lighthousePos;
    float maxDistance;

    [SerializeField] float speed = 10.0f;

    MeshFilter meshFilter;
    GameObject limit;


    void Start()
    {
        GameObject lighthouse = GameObject.FindGameObjectWithTag("Faro");
        lighthousePos = lighthouse.transform.position;
        limit = lighthouse.transform.Find("Limit").gameObject;
        meshFilter = limit.GetComponent<MeshFilter>();
        luz = GetComponent<Light>();
        newPosition = transform.position;
        UpdateMaxDistance();
    }

    public IEnumerator ScaleRange(float percentIncrease, float time)
    {
        float currentAngle = luz.spotAngle;
        float currentInnerAngle = luz.innerSpotAngle;
        float currentTime = 0.0f;

        while (currentTime <= time)
        {
            luz.spotAngle = Mathf.Lerp(currentAngle, currentAngle * percentIncrease, currentTime / time);
            luz.innerSpotAngle = Mathf.Lerp(currentInnerAngle, currentInnerAngle * percentIncrease, currentTime / time);
            yield return null;
            currentTime += Time.deltaTime;
        }
    }

    public float GetRadius()
    {
        return Mathf.Tan(luz.spotAngle / 2.0f) * transform.position.y;
    }

    public void UpdateMaxDistance()
    {
        Vector3[] vertexList = meshFilter.sharedMesh.vertices;
        Vector3 centerMesh = (limit.transform.TransformPoint(vertexList[0]) + limit.transform.TransformPoint(vertexList[10])) / 2.0f;
        centerMesh.y = 0;
        Vector3 posLight = lighthousePos;
        posLight.y = 0;
        maxDistance = Vector3.Distance(centerMesh, posLight);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NewDestinyPos();
        }

        MoveTowardsDestiny();
    }

    private void MoveTowardsDestiny()
    {
        transform.position = Vector3.MoveTowards(transform.position,newPosition,speed * Time.deltaTime);
    }

    void NewDestinyPos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector2 v1 = new Vector2(hit.point.x, hit.point.z);
            Vector2 v2 = new Vector2(lighthousePos.x, lighthousePos.z);
            if (Vector2.Distance(v1, v2) < maxDistance)
            {
                newPosition = hit.point;
                newPosition.y = 4.0f;
            }          
            
        }
    }
}
