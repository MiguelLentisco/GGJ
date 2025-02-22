﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopManager : MonoBehaviour
{
    [SerializeField]
    Text moneyText;
    [SerializeField]
    Text warningNotEnoughMoney;
    [SerializeField] // [UI] El texto que muestra la cantidad de powerups que tienes
    Text[] textAmountPowerUp;
    [SerializeField] // [UI] El texto que muestra el valor de los powerups
    Text[] textCostPowerUp;


    int[] costPowerUp = { 50, 100, 200, 300};
    int[] amountPowerUp = new int[(int)PowerUp.NPOWERUPS - 1];

    float savedMoney;
    GameManager GM;
    LevelManager LM;

    void Start()
    {
        // Set saved Data
        
    }

    private void OnEnable()
    {

        GM = GameManager.instance;
        LM = FindObjectOfType<LevelManager>();
        LM.setCanPause(false);
        LM.setCanHUD(false);

        for (int i = 0; i < costPowerUp.Length; i++)
            textCostPowerUp[i].text = "$" + costPowerUp[i].ToString();

        savedMoney = GM.dinero;
        amountPowerUp[0] = GM.GetNumberPowerUps(PowerUp.SlowdownShips);
        amountPowerUp[1] = GM.GetNumberPowerUps(PowerUp.IncreaseLight);
        amountPowerUp[2] = GM.GetNumberPowerUps(PowerUp.IncreaseLimit);
        amountPowerUp[3] = GM.GetNumberPowerUps(PowerUp.VisionMap);
    }

    // Update is called once per frame
    void Update()
    {
       
        // Update all data
        moneyText.text = "$" + savedMoney.ToString();
        for (int i = 0; i < amountPowerUp.Length; i++)
            textAmountPowerUp[i].text = amountPowerUp[i].ToString();
    }

    public void buyPowerUp(int powerup)
    {
        if(savedMoney >= costPowerUp[powerup]) // Comprar powerup
        {
            savedMoney -= costPowerUp[powerup];
            amountPowerUp[powerup] = amountPowerUp[powerup] + 1;
        } else // Avisar de que no hay dinero suficiente
        {
            //StartCoroutine(warningForSeconds(4));
        }
    }

    public void setAmountPowerUp(int[] pArrayAmount)
    {
        for (int i = 0; i < pArrayAmount.Length; i++)
            amountPowerUp[i] = pArrayAmount[i];
    }

    public void finishShopping()
    {
        sendDataToGameManager();
        // Llamar a LevelManager para cambiar de nivel
        GM.IniciaRonda();
        this.gameObject.SetActive(false);
    }

    IEnumerator warningForSeconds(int sec)
    {
        warningNotEnoughMoney.gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);
        warningNotEnoughMoney.gameObject.SetActive(false);
    }

    void sendDataToGameManager()
    {
        GM.dinero = savedMoney;
        GM.AddPowerUp(PowerUp.SlowdownShips, amountPowerUp[0]);
        GM.AddPowerUp(PowerUp.IncreaseLight, amountPowerUp[1]);
        GM.AddPowerUp(PowerUp.IncreaseLimit, amountPowerUp[2]);
        GM.AddPowerUp(PowerUp.VisionMap, amountPowerUp[3]);
    }

    
}
