using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1f;
    public float spawnDistance = 5f;
    public float speed = 1f;
    public int health = 1;
    public List<Player> players;
    public List<Enemy> enemies;

    public static EnemySpawner Instance { get; private set; }
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        enemies = new List<Enemy>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        // Find all players in the scene
        players = FindObjectsByType<Player>(FindObjectsSortMode.None).ToList();

        // Start spawning enemies
        foreach (var player in players) {
            StartCoroutine(SpawnEnemies(player));
        }
    }

    private IEnumerator SpawnEnemies(Player player) {
        while (true) {
            // Spawn an enemy
            SpawnEnemy(player);
            // Wait for 1/spawnRate seconds before spawning the next enemy
            yield return new WaitForSeconds(1/spawnRate);
        }
    }

    private void SpawnEnemy(Player player) {
        // Spawn an Enemy at a random position within spawnRadius centered on the player
        Vector2 spawnPosition = (player != null ? player.Position : Vector2.zero) + Random.insideUnitCircle.normalized * spawnDistance;
        Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Enemy>();
        enemy.SetTarget(player.transform);
        enemy.SetSpeed(speed);
        enemy.SetHealth(health);
        enemies.Add(enemy);
    }

    public void StopSpawning() {
        StopAllCoroutines();
    }
}
