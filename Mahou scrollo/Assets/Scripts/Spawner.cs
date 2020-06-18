using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private Score score;

    private Coroutine SpawnTimerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTimerCoroutine = StartCoroutine(SpawnTimer(timeBetweenSpawns));
    }

    private void Spawn()
    {
        Transform spawnpoint = ChooseSpawnpoint();
        if (spawnpoint != null)
            Instantiate(ChooseObjectToSpawn(), spawnpoint.position, spawnpoint.rotation)
                .GetComponentInChildren<Health>().score = score;
    }

    private GameObject ChooseObjectToSpawn()
    {
        return objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
    }

    private Transform ChooseSpawnpoint()
    {
        Transform spawnpoint;
        spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];

        if (spawnpoint.GetComponent<NearSpawnpointDetector>().SomethingIsNear)
        {
            for (int i = 0; i < spawnpoints.Length; ++i)
            {
                spawnpoint = spawnpoints[i];
                if (!spawnpoint.GetComponent<NearSpawnpointDetector>().SomethingIsNear)
                {
                    return spawnpoint;
                }
            }
            return null;
        }

        return spawnpoint;
    }

    private IEnumerator SpawnTimer(float timeBetweenSpawns)
    {
        WaitForSeconds time = new WaitForSeconds(timeBetweenSpawns);

        while (true)
        {
            yield return time;

            Spawn();
        }
    }
}
