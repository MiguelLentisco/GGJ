using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    [SerializeField] PowerUp kind;
    [SerializeField] Text numberOfPowerUps;
    int amount;

    // Start is called before the first frame update
    void Start()
    {
        amount = GameManager.instance.GetNumberPowerUps(kind);

        numberOfPowerUps.text = amount.ToString();

        if (amount == 0)
        {
            DeactivateButton();
        }
    }

    public void ActivatePowerUp()
    {
        switch (kind)
        {
            case PowerUp.IncreaseLight:
                GameManager.instance.IncreaseLightPowerUp();
                break;
            case PowerUp.IncreaseLimit:
                GameManager.instance.IncreaseRangePowerUp();
                break;
            case PowerUp.ShieldShip:
                GameManager.instance.ShieldPowerUp();
                break;
            case PowerUp.SlowdownShips:
                GameManager.instance.SlowdownPowerUp();
                break;
            case PowerUp.VisionMap:
                GameManager.instance.ShowMapPowerUp();
                break;
        }
    }

    public void DeactivateButton()
    {
        GetComponent<Button>().interactable = false;
    }

    
}
