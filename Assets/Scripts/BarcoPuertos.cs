using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoPuertos : MonoBehaviour
{
    ColorBarco colorBarco;

    [SerializeField] float dineroBarco = 100f;

    [SerializeField]
    GameObject winparticle;

    [SerializeField]
    GameObject sounds;

    private void Start()
    {
        colorBarco = GetComponent<BarcoLuz>().colorBarco;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PuebloAzul") || other.gameObject.CompareTag("PuebloRojo") 
            || other.gameObject.CompareTag("PuebloAmarillo") || other.gameObject.CompareTag("PuebloVerde"))
        {
            GameManager.instance.AddMoney(HowMuchMoneyIEarned(other.tag));
            Instantiate(winparticle, transform.position, Quaternion.identity);
            GameObject newSound = Instantiate(sounds, transform.position, Quaternion.identity);
            newSound.GetComponent<BoatSoundManager>().PlayWinpoint();
            Destroy(this.gameObject);
        }
    }

    private float HowMuchMoneyIEarned(string tag)
    {
        switch (colorBarco)
        {
            case ColorBarco.AMARILLO:
                if (tag == "PuebloAmarillo")
                {
                    return dineroBarco;
                }
                break;
            case ColorBarco.AZUL:
                if (tag == "PuebloAzul")
                {
                    return dineroBarco;
                }
                break;
            case ColorBarco.ROJO:
                if (tag == "PuebloRojo")
                {
                    return dineroBarco;
                }
                break;
            case ColorBarco.VERDE:
                if (tag == "PuebloVerde")
                {
                    return dineroBarco;
                }
                break;
        }

        return dineroBarco/2;
    }
}
