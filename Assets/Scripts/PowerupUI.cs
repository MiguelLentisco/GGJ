using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    [SerializeField] PowerUp kind;
    [SerializeField] Text numberOfPowerUps;

    Image image;
    int amount;

    float duration = 10f;

    bool isInCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        amount = GameManager.instance.GetNumberPowerUps(kind);
        image = GetComponent<Image>();

        numberOfPowerUps.text = amount.ToString();

        if (amount == 0)
        {
            DeactivateButton();
        }

        GetPowerUpSeconds();
    }

    private void Update()
    {
        CalculateFillAmount();
    }

    private void CalculateFillAmount()
    {
        if (isInCooldown)
        {
            image.fillAmount += 1 / duration * Time.deltaTime;

            if (image.fillAmount >= 1)
            {
                isInCooldown = false;
            }
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

        isInCooldown = true;
        image.fillAmount = 0f;
    }

    public void DeactivateButton()
    {
        GetComponent<Button>().interactable = false;
    }

    public void GetPowerUpSeconds()
    {
        switch (kind)
        {
            case PowerUp.IncreaseLight:
                duration = 5f;
                break;
            case PowerUp.IncreaseLimit:
                duration = 5f;
                break;
            case PowerUp.ShieldShip:
                duration = 5f;
                break;
            case PowerUp.SlowdownShips:
                duration = 5f;
                break;
            case PowerUp.VisionMap:
                duration = 5f;
                break;
        }
    }
}
