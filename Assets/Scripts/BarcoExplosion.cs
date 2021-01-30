using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoExplosion : MonoBehaviour
{
    [SerializeField]
    GameObject boom;

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
            Instantiate(boom, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
