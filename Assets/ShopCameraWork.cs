using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCameraWork : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float velocity;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, velocity * Time.deltaTime);
    }
}
