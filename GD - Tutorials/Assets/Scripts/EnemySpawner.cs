using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesToSpawn;
    [SerializeField] public List<GameObject> enemies;

    [SerializeField] float spawnTime;

    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        enemies.Add(Instantiate(enemiesToSpawn[0], transform));

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnTime);

        int randomEnemy = Random.Range(0, enemiesToSpawn.Count);

        enemies.Add(Instantiate(enemiesToSpawn[randomEnemy]));

        StartCoroutine(SpawnEnemy());
    }

    public void DestroyAllEnemies()
    {
        StopAllCoroutines();

        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        enemies.Clear();
    }
}