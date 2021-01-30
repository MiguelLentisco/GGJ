using UnityEngine;

public class BarcoLuz : MonoBehaviour
{
    [SerializeField]
    Light lightBoat;
    [SerializeField]
    float intervalSeconds;

    bool apagado = false;

    private void OnEnable()
    {
        InvokeRepeating("blinkingLight", 0, intervalSeconds);
    }

    private void OnDisable()
    {
        CancelInvoke("blinkingLight");
    }

    private void blinkingLight()
    {
        lightBoat.intensity = apagado? 1.0f : 0.0f;
        apagado = !apagado;
    }
}
