using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    SpawnBoats boatsSpawner = null;
    Lighthouse lighthouse = null;

    [SerializeField]
    float increasedLighthouseRadius = 5.0f;
    [SerializeField]
    float timeIncreaseLighthouseRadius = 10.0f;

    [SerializeField]
    float slowdownBoatsPercent = 0.5f;
    [SerializeField]
    float timeSlowdownBoats = 10.0f;

    int rondaActual = 0;
    int nBarcosRestantes = 0;
    float dinero = 0.0f;

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
        boatsSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnBoats>();
        lighthouse = GameObject.FindGameObjectWithTag("Faro").GetComponent<Lighthouse>();
        AvanzaRonda();
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
        yield return lighthouse.ScaleUpOverTime(increasedLighthouseRadius);
        yield return new WaitForSeconds(timeIncreaseLighthouseRadius);
        yield return lighthouse.ScaleDownOverTime(increasedLighthouseRadius);
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

    void BarcoPerdido()
    {
        --nBarcosRestantes;
        if (nBarcosRestantes == 0)
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
}
