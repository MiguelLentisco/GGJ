using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoExplosion : MonoBehaviour
{
    [SerializeField]
    GameObject boom;

    [SerializeField]
    GameObject sounds;

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
        if (collision.gameObject.CompareTag("Barco") || collision.gameObject.CompareTag("Obstaculo"))
        {
            GameManager.instance.BarcoPerdido(1);
            Instantiate(boom, transform.position, Quaternion.identity);
            GameObject newSound = Instantiate(sounds, transform.position, Quaternion.identity);
            newSound.GetComponent<BoatSoundManager>().PlayExplosion();
            Destroy(this.gameObject);
        }
    }
}
