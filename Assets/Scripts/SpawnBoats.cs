
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnBoats : MonoBehaviour
{
    [System.Serializable]
    struct Barco
    {
        public GameObject prefabBarco;
        public float proporcionSpawn;
    }

    [System.Serializable]
    struct SpawnPoint
    {
        public Transform center;
        public float rX;
        public float rZ;
    }

    [SerializeField]
    Barco[] tiposBarcos = null;
    [SerializeField]
    SpawnPoint[] puntosSpawn = null;
    Vector3 faro;

    [SerializeField]
    float minTimeToSpawn = 0.0f;
    [SerializeField]
    float maxTimeToSpawn = 10.0f;

    void Start()
    {
        System.Array.Sort(tiposBarcos, delegate (Barco x, Barco y) { return x.proporcionSpawn.CompareTo(y.proporcionSpawn); });
        faro = GameObject.FindGameObjectWithTag("Faro").transform.position;
    }

    int NumberBoatsToSpawn(int round)
    {
        return round * (round + 1) / 2;
    }

    IEnumerator SpawnEnemies(int round)
    {
        int totalBoatsToSpawn = NumberBoatsToSpawn(round);
        int nBoatsToSpawn = totalBoatsToSpawn;

        foreach (Barco tipoBarco in tiposBarcos)
        {
            int nSubBoatsToSpawn = Mathf.RoundToInt(totalBoatsToSpawn * tipoBarco.proporcionSpawn);
            while (nSubBoatsToSpawn > 0 && nBoatsToSpawn > 0)
            {
                yield return generateBoat(tipoBarco.prefabBarco);
                --nSubBoatsToSpawn;
                --nBoatsToSpawn;
            }
        }

        Assert.IsTrue(nBoatsToSpawn <= tiposBarcos.Length);
        for (int i = 0; i < nBoatsToSpawn; ++i)
            yield return generateBoat(tiposBarcos[i].prefabBarco);
    }

    IEnumerator generateBoat(GameObject prefabBarco)
    {
        SpawnPoint puntoSpawn = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
        Vector3 pos = puntoSpawn.center.position;
        yield return new WaitForSeconds(Random.Range(minTimeToSpawn, maxTimeToSpawn));
        pos.x = Random.Range(pos.x - puntoSpawn.rX, pos.x + puntoSpawn.rX);
        pos.z = Random.Range(pos.z - puntoSpawn.rZ, pos.z + puntoSpawn.rZ);
        GameObject bote = Instantiate(prefabBarco, pos, Quaternion.identity);
        bote.transform.LookAt(faro);
    }

    public void SpawnEnemiesInRound(int round)
    {
        StartCoroutine(SpawnEnemies(round));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
