using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DowngradeSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> downgradeToSpawn;
    [SerializeField] public List<GameObject> downgrades;
    [SerializeField] public List<Transform> placesToSpawn;

    [SerializeField] float spawnTime;

    public static DowngradeSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnDowngrade());
    }

    IEnumerator SpawnDowngrade()
    {
        yield return new WaitForSeconds(spawnTime);

        int randomDowngrade = Random.Range(0, downgradeToSpawn.Count);
        int randomPlaces = Random.Range(0, placesToSpawn.Count);

        downgrades.Add(Instantiate(downgradeToSpawn[randomDowngrade], placesToSpawn[randomPlaces]));
        downgradeToSpawn[randomDowngrade].transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(30);

        Destroy(downgrades[downgrades.Count - 1]);

        StartCoroutine(SpawnDowngrade());
    }

    public void DestroyAllDowngrades()
    {
        StopAllCoroutines();

        foreach (var downgrade in downgrades)
        {
            Destroy(downgrade);
        }

        downgrades.Clear();
    }
}