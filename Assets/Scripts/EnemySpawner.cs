using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDistance = 5f;
    public float speed = 1f;
    public int health = 1;
    public int maxEnemies = 10;
    public List<Player> players;
    public List<Enemy> enemies;

    public static EnemySpawner Instance { get; private set; }
    [SerializeField]
    private double difficultyFactor = 1f;
    [SerializeField]
    private float difficultyIncreaseStep = 0.1f;
    [SerializeField]
    private float difficultyIncreaseTime = 30f;
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

        // Start increasing difficulty over time
        StartCoroutine(IncreaseDifficultyOverTime());
    }

    private IEnumerator SpawnEnemies(Player player) {
        while (true) {
            // Check if the number of enemies is less than maxEnemies
            if (enemies.Count < maxEnemies) {
                // Spawn an enemy
                SpawnEnemy(player);
            }
            // Wait for a short duration before checking again
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SpawnEnemy(Player player) {
        // Spawn an Enemy at a random position within spawnRadius centered on the player
        Vector2 spawnPosition = (player != null ? player.Position : Vector2.zero) + Random.insideUnitCircle.normalized * spawnDistance;
        Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Enemy>();
        enemy.SetTarget(player.transform);
        enemy.SetSpeed(speed);
        enemy.SetHealth((int)(health * difficultyFactor));
        enemies.Add(enemy);
    }

    private IEnumerator IncreaseDifficultyOverTime() {
        while (true) {
            yield return new WaitForSeconds(difficultyIncreaseTime);
            difficultyFactor += difficultyIncreaseStep;
        }
    }

    public void StopSpawning() {
        StopAllCoroutines();
    }
}