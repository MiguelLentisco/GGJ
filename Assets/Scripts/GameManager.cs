using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField]
    float increaseRadiusLimit = 3.0f;
    [SerializeField]
    float timeIncreaseRadiusLimit = 2.5f;
    [SerializeField]
    float durationIncreaseRadiusLimit = 10.0f;

    [SerializeField]
    float slowdownBoatsPercent = 0.5f;
    [SerializeField]
    float timeSlowdownBoats = 10.0f;

    int rondaActual = 0;
    int nBarcosRestantes = 0;
    public float dinero = 0.0f;

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

        lighthouse = GameObject.FindGameObjectWithTag("Faro").GetComponent<Lighthouse>();
        boatsSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnBoats>();
        //AvanzaRonda();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartPUSlowdownBoats();
        else if (Input.GetKeyDown(KeyCode.D))
            StartPUIncreaseLighthouseRaidus();
        else if (Input.GetKeyDown(KeyCode.W))
            SpawnBoats();
    }

    void SpawnBoats()
    {
        boatsSpawner.SpawnEnemiesInRound(rondaActual);
    }

    public void StartPUIncreaseLighthouseRaidus()
    {
        StartCoroutine(UsePUIncreaseLighthouseRadius());
    }

    IEnumerator UsePUIncreaseLighthouseRadius()
    {
        yield return lighthouse.ScaleUpOverTime(increaseRadiusLimit, timeIncreaseRadiusLimit);
        yield return new WaitForSeconds(durationIncreaseRadiusLimit);
        yield return lighthouse.ScaleDownOverTime(timeIncreaseRadiusLimit);
    }

    public void StartPUSlowdownBoats()
    {
        StartCoroutine(UsePUSlowdownBoats());
    }

    IEnumerator UsePUSlowdownBoats()
    {
        GameObject[] boats = GameObject.FindGameObjectsWithTag("Barco");
        foreach (GameObject boat in boats)
        {
            boat.GetComponent<BarcoMovement>().UpdateSpeedPercent(slowdownBoatsPercent);
        }
        yield return new WaitForSeconds(timeSlowdownBoats);
        foreach (GameObject boat in boats)
        {
            boat.GetComponent<BarcoMovement>().UpdateSpeedPercent(1.0f);
        }
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

    void AvanzaRonda()
    {
        ++rondaActual;
        nBarcosRestantes = rondaActual * (rondaActual + 1) / 2;
        SpawnBoats();
    }


    public void AddPowerUp(PowerUp powerup, int cantidad)
    {
        powerUpsAvailable[(int) powerup] += cantidad;
    }

    public int GetNumberPowerUps(PowerUp powerup)
    {
        return powerUpsAvailable[(int) powerup];
    }
}
