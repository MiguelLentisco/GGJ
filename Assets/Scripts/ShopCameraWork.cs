using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCameraWork : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float velocity;
    [SerializeField]
    SpawnBoats SB;

    private void Start()
    {
        SB.SpawnEnemiesInRound(5);
    }

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, velocity * Time.deltaTime);
        transform.LookAt(target.position);
    }
}
