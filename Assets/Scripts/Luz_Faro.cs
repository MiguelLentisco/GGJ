using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luz_Faro : MonoBehaviour
{
    Vector3 newPosition;
    [SerializeField] float speed = 5.0f;
    
    void Start()
    {
        newPosition = transform.position;
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
            newPosition = hit.point;
            newPosition.y = 4.0f;
        }
    }
}
