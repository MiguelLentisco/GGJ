using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoMovement : MonoBehaviour
{
    Transform luz = null;
    [SerializeField]
    float radioDeteccion = 30.0f;
    [SerializeField]
    float radioAcercamiento = 1.0f;
    [SerializeField]
    float velocidad = 10.0f;
    [SerializeField]
    float velocidadRotacion = 1.0f;

    // Private
    float radioDeteccionSqr;
    float radioAcercamientoSqr;
    Vector3 ultimaPosicionLuz;
    bool continuar = false;
    float speedPercent = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        luz = GameObject.FindGameObjectWithTag("Luz").transform;
        ultimaPosicionLuz = luz.position;
        radioDeteccionSqr = radioDeteccion * radioDeteccion;
        radioAcercamientoSqr = radioAcercamiento * radioAcercamiento;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = luz.position - transform.position;
        dir.y = 0;
        if (ultimaPosicionLuz == luz.position)
        {
            if (!continuar && dir.sqrMagnitude < radioAcercamientoSqr)
                continuar = true;
        }
        else
        {
            ultimaPosicionLuz = luz.position;
            continuar = false;
        }

        if (!continuar && dir.sqrMagnitude < radioDeteccionSqr)
        {
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * velocidadRotacion * speedPercent);
        }

        transform.position += transform.forward * velocidad * speedPercent * Time.deltaTime;
    }
    
    public void UpdateSpeedPercent(float speedPercent)
    {
        this.speedPercent = speedPercent;
    }
}
