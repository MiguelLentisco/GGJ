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
    
    void Start()
    {
        lighthousePos = GameObject.FindGameObjectWithTag("Faro").transform.position;
        luz = GetComponent<Light>();
        newPosition = transform.position;
    }

    public void ChangeDistance(float newDistance)
    {
        maxDistance = newDistance;
    }

    public float GetRadius()
    {
        return Mathf.Tan(luz.spotAngle / 2.0f) * transform.position.y;
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
            Debug.Log(hit.point);
            Vector2 v1 = new Vector2(hit.point.x, hit.point.z);
            Vector2 v2 = new Vector2(lighthousePos.x, lighthousePos.z);
            Debug.Log(Vector2.Distance(v1, v2));
            if (Vector2.Distance(v1, v2) < maxDistance)
            {
                newPosition = hit.point;
                newPosition.y = 4.0f;
            }          
            
        }
    }
}
