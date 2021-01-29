﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoMovement : MonoBehaviour
{
    [SerializeField]
    Transform luz;
    [SerializeField]
    float radioDeteccion = 30.0f;
    [SerializeField]
    float radioAcercamiento = 1.0f;
    [SerializeField]
    float velocidad = 10.0f;
    [SerializeField]
    float velocidadMaxima = 10.0f;
    [SerializeField]
    float velocidadRotacion = 1.0f;

    // Private
    Rigidbody rb;
    float radioDeteccionSqr;
    float radioAcercamientoSqr;
    Vector3 ultimaPosicionLuz;
    bool continuar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * velocidadRotacion);
        }

        transform.position += transform.forward * velocidad * Time.deltaTime;
    }
}
