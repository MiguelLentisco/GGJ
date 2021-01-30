using UnityEngine;

[System.Serializable]
public enum ColorBarco
{
    ROJO,
    VERDE,
    AMARILLO,
    AZUL
}


public class BarcoLuz : MonoBehaviour
{
    Light lightBoat;

    [SerializeField]
    float intervalSeconds;

    [SerializeField]
    Color luzRoja;
    [SerializeField]
    Color luzVerde;
    [SerializeField]
    Color luzAmarilla;
    [SerializeField]
    Color luzAzul;

    ColorBarco colorBarco;
    bool apagado = false;

    private void Start()
    {
        System.Array tiposLuz = System.Enum.GetValues(typeof(ColorBarco));
        colorBarco = (ColorBarco) tiposLuz.GetValue(Random.Range(0, tiposLuz.Length));
        lightBoat = transform.Find("Light").GetComponent<Light>();
        lightBoat.color = LuzToColor(colorBarco);
    }
    private void OnEnable()
    {
        InvokeRepeating("BlinkingLight", 0, intervalSeconds);
    }

    private void OnDisable()
    {
        CancelInvoke("BlinkingLight");
    }

    private void BlinkingLight()
    {
        lightBoat.intensity = apagado? 1.0f : 0.0f;
        apagado = !apagado;
    }

    Color LuzToColor(ColorBarco colorBarco)
    {
        switch (colorBarco)
        {
            case ColorBarco.AMARILLO:
                return luzAmarilla;
            case ColorBarco.AZUL:
                return luzAzul;
            case ColorBarco.ROJO:
                return luzRoja;
            case ColorBarco.VERDE:
                return luzVerde;
        }
        return Color.white;
    }

}
