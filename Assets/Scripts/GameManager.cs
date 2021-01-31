﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

[System.Serializable]
public enum PowerUp
{
    IncreaseLimit,
    IncreaseLight,
    SlowdownShips,
    VisionMap,
    ShieldShip,
    NPOWERUPS
};


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    SpawnBoats boatsSpawner = null;
    Lighthouse lighthouse = null;
    LuzFaro luzFaro = null;

    [SerializeField]
    float increaseRadiusLimit = 1.5f;
    [SerializeField]
    float timeIncreaseRadiusLimit = 2.5f;
    [SerializeField]
    float durationIncreaseRadiusLimit = 10.0f;

    [SerializeField]
    float increaseLightRadius = 1.5f;
    [SerializeField]
    float timeIncreaseLightRadius = 2.5f;
    [SerializeField]
    float durationIncreaseLightRadius = 10.0f;

    [SerializeField]
    float slowdownBoatsPercent = 0.5f;
    [SerializeField]
    float durationSlowdownBoats = 10.0f;

    [SerializeField]
    float increaseIntensity = 1.15f;
    [SerializeField]
    float timeIntensity = 1.0f;
    [SerializeField]
    float durationIncreaseIntensity = 10.0f;

    [SerializeField]
    Text moneyText;
    LevelManager levelManager;
    LightChange luzGlobal = null;

    int rondaActual = 0;
    int nBarcosRestantes = 0;
    public float dinero = 0.0f;
    bool jugando = false;

    int[] powerUpsAvailable = new int[(int) PowerUp.NPOWERUPS];


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)PowerUp.NPOWERUPS; ++i)
            powerUpsAvailable[i] = 0;  
    }

    public void IniciarJuego()
    {
        jugando = false;
        dinero = 0.0f;
        lighthouse = GameObject.FindGameObjectWithTag("Faro").GetComponent<Lighthouse>();
        luzFaro = GameObject.FindGameObjectWithTag("Luz").GetComponent<LuzFaro>();
        boatsSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnBoats>();
        moneyText.text = dinero.ToString() + "$";
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        luzGlobal = GameObject.FindGameObjectWithTag("LuzGlobal").GetComponent<LightChange>();
        IniciaRonda();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugando) 
        {
            if (GameObject.FindGameObjectWithTag("Barco") == null)
                AcabarRonda();
        }
    }

    void AcabarRonda()
    {
        Debug.Log("se terminó el juego");
        jugando = false;
        
    }

    void SpawnBoats()
    {
        jugando = true;
        boatsSpawner.SpawnEnemiesInRound(rondaActual);
    }

    IEnumerator UsePUIncreaseLighthouseRadius()
    {
        yield return lighthouse.ScaleOverTime(increaseRadiusLimit, timeIncreaseRadiusLimit);
        yield return new WaitForSeconds(durationIncreaseRadiusLimit);
        yield return lighthouse.ScaleOverTime(1.0f / increaseRadiusLimit, timeIncreaseRadiusLimit);
    }


    IEnumerator UsePUSlowdownBoats()
    {
        GameObject[] boats = GameObject.FindGameObjectsWithTag("Barco");
        foreach (GameObject boat in boats)
        {
            boat.GetComponent<BarcoMovement>().UpdateSpeedPercent(slowdownBoatsPercent);
        }
        yield return new WaitForSeconds(durationSlowdownBoats);
        foreach (GameObject boat in boats)
        {
            boat.GetComponent<BarcoMovement>().UpdateSpeedPercent(1.0f);
        }
    }

    IEnumerator UsePUIncreaseLightRadius()
    {
        yield return luzFaro.ScaleRange(increaseLightRadius, timeIncreaseLightRadius);
        yield return new WaitForSeconds(durationIncreaseLightRadius);
        yield return luzFaro.ScaleRange(1.0f / increaseLightRadius, timeIncreaseLightRadius);
    }

    IEnumerator UsePUClearFog()
    {
        yield return luzGlobal.changeIntensity(increaseIntensity, timeIntensity);
        yield return new WaitForSeconds(durationIncreaseIntensity);
        yield return luzGlobal.changeIntensity(1.0f / increaseIntensity, timeIntensity);
    }

    public void BarcoPerdido(int nBarcosPerdidos)
    {
        nBarcosRestantes = Mathf.Max(0, nBarcosRestantes - nBarcosPerdidos);
        if (nBarcosPerdidos == 0)
            PlayerPierde();
    }

    void PlayerPierde()
    {

    }

    public void IniciaRonda()
    {
        ++rondaActual;
        nBarcosRestantes = rondaActual * (rondaActual + 1) / 2;
        SpawnBoats();
        levelManager.updateRounds();
    }


    public void AddPowerUp(PowerUp powerup, int cantidad)
    {
        powerUpsAvailable[(int) powerup] += cantidad;
    }

    public int GetNumberPowerUps(PowerUp powerup)
    {
        return powerUpsAvailable[(int) powerup];
    }

    public void AddMoney(float amount)
    {
        dinero += amount;
        moneyText.text = dinero.ToString() + "$";
    }

    public void ShieldPowerUp()
    {

    }

    public void IncreaseRangePowerUp()
    {
        StartCoroutine(UsePUIncreaseLighthouseRadius());
    }

    public void IncreaseLightPowerUp()
    {
        StartCoroutine(UsePUIncreaseLightRadius());
    }

    public int GetRounds()
    {
        return rondaActual;
    }

    public void ClearFogPowerUp()
    {
        StartCoroutine(UsePUClearFog());
    }

    public void SlowdownPowerUp()
    {
        StartCoroutine(UsePUSlowdownBoats());
    }
}
