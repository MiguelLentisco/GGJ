using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barco_Movement : MonoBehaviour
{
    // Public
    public Transform luz;
    public bool luzEncontrada;
    public float radioDeteccion;
    public float velocidad;
    public float velocidadMaxima;

    // Private
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Barco ha encontrado la luz
        if (luzEncontrada)
        {
            Vector3 dir = luz.position - transform.position;
            dir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
        }

        // La luz está en la detección del barco
        if (Vector3.Distance(transform.position, luz.transform.position) < radioDeteccion) { 
            luzEncontrada = true;
        } else
        {
            luzEncontrada = false;
        }

        // El barco se mueve en dirección al forward vector
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude < velocidadMaxima) { 
            rb.AddForce(velocidad * transform.forward);
        }
    }
}
