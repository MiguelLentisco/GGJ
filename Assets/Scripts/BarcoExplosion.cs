using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoExplosion : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barco"))
        {
            GameManager.instance.BarcoPerdido(1);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
