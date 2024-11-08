using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> powerupsToSpawn;
    [SerializeField] public List<GameObject> powerups;
    [SerializeField] public List<Transform> placesToSpawn;

    [SerializeField] float spawnTime;

    public static PowerupSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(spawnTime);

        int randomPowerup = Random.Range(0, powerupsToSpawn.Count);
        int randomPlaces = Random.Range(0, placesToSpawn.Count);

        powerups.Add(Instantiate(powerupsToSpawn[randomPowerup], placesToSpawn[randomPlaces]));
        powerupsToSpawn[randomPowerup].transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(30);

        Destroy(powerups[powerups.Count - 1]);

        StartCoroutine(SpawnPowerup());
    }

    public void DestroyAllPowerups()
    {
        StopAllCoroutines();

        foreach (var powerup in powerups)
        {
            Destroy(powerup);
        }

        powerups.Clear();
    }
}