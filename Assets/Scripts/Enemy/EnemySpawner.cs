using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private float spawnRadius = 5.0f; // Radius within which enemies can spawn
    [SerializeField] private float minDistanceBetweenEnemies = 2.0f; // Minimum distance between enemies
    private List<Enemy> enemies = new List<Enemy>();
    private void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
        
        StartCoroutine(SpawnEnemies());
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        if (spawnPosition != Vector3.zero)
        {
            Enemy newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
            newEnemy.enemySpawner = this;
            enemies.Add(newEnemy);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (enemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        bool validPositionFound = false;

        for (int i = 0; i < 10; i++) // Try to find a valid position up to 10 times
        {
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            randomPosition = new Vector3(randomPoint.x, 0, randomPoint.y) + transform.position;

            if (IsPositionValid(randomPosition))
            {
                validPositionFound = true;
                break;
            }
        }

        if (validPositionFound)
        {
            return randomPosition;
        }
        else
        {
            return Vector3.zero; // Failed to find a valid position
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (Enemy enemy in enemies)
        {
            if (Vector3.Distance(position, enemy.transform.position) < minDistanceBetweenEnemies)
            {
                return false; // Position is not valid as it is too close to another enemy
            }
        }

        return true;
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
}