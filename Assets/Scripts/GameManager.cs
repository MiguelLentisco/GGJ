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
        //Assert.IsNotNull(boatsSpawner);
        lighthouse = GameObject.FindGameObjectWithTag("Faro").GetComponent<Lighthouse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartPUSlowdownBoats();
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
}
