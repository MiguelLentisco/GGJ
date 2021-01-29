using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoats : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBoat;

    [SerializeField]
    float spawnRadiusX = 100.0f;
    [SerializeField]
    float spawnRadiusZ = 100.0f;

    [SerializeField]
    float minTimeToSpawn = 0.0f;
    [SerializeField]
    float maxTimeToSpawn = 10.0f;

    bool spawning = false;

    int NumberBoatsToSpawn(int round)
    {
        return round * (round + 1) / 2;
    }

    IEnumerator SpawnEnemies(int round)
    {
        spawning = true;
        int nEnemiesToSpawn = NumberBoatsToSpawn(round);
        while (nEnemiesToSpawn > 0)
        {
            yield return new WaitForSeconds(Random.Range(minTimeToSpawn, maxTimeToSpawn));
            Vector3 posSpawn = transform.position;
            posSpawn.x += Random.Range(posSpawn.x - spawnRadiusX, posSpawn.x + spawnRadiusX);
            posSpawn.z += Random.Range(posSpawn.z - spawnRadiusZ, posSpawn.z + spawnRadiusZ);
            Instantiate(prefabBoat, posSpawn, Quaternion.identity);
            --nEnemiesToSpawn;
        }
        Debug.Log(nEnemiesToSpawn);
        spawning = false;
    }

    void SpawnEnemiesInRound(int round)
    {
        StartCoroutine(SpawnEnemies(round));
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning && Input.GetKeyDown(KeyCode.A))
            SpawnEnemiesInRound(3);
    }
}
