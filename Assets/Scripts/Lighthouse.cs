using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    [SerializeField] Transform lightPos;
    Quaternion originalRot;

    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(lightPos.position.x, transform.position.y, lightPos.position.z));
    }
}
