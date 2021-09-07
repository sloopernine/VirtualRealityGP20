using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int numberToSpawn;


    private void Start()
    {
        SpawnEnemies(enemyPrefab, numberToSpawn);
    }

    private void SpawnEnemies(GameObject enemyType, int spawnNumber)
    {
        //if (enemyType.GetComponent<Enemy>() == null)
        //{
        //    Debug.LogWarning("You added something else than a enemy to the Enemy Spawner");
        //}
        for (int i = 0; i < spawnNumber; i++)
        {
            Debug.Log("Trying to spawn one enemy");
            Instantiate(enemyType, transform.position, transform.rotation);
        }
    }
}
