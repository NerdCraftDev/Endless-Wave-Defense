using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerUpgrades playerUpgrades;
    public int health = 5;
    public Vector2 Position => transform.position;
    public List<Attack> attacks;

    private void Awake()
    {
        // Get the PlayerMovement component
        movement = GetComponent<PlayerMovement>();
        if (movement == null)
        {
            movement = gameObject.AddComponent<PlayerMovement>();
        }
        // Get the PlayerUpgrades component
        playerUpgrades = GetComponent<PlayerUpgrades>();
        if (playerUpgrades == null)
        {
            playerUpgrades = gameObject.AddComponent<PlayerUpgrades>();
        }
        foreach (var attack in attacks)
        {
            attack.Initialize(gameObject);
        }
    }

    private void Update()
    {
        // If the player presses the space key, call Attack()
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    public void Move(Vector2 direction)
    {
        movement.Move(direction);
    }

    public void Attack() {
        foreach (var attack in attacks)
        {
            attack.Use(gameObject);
        }
    }

    public void ShowUpgradeOptions()
    {
        playerUpgrades.ShowUpgradeOptions();
    }

    public void SetSpeed(float speed)
    {
        movement.SetSpeed(speed);
    }

    public void Damage(int damage)
    {
        // Reduce the player's health by damage
        health -= damage;
        // If the player's health is less than or equal to 0, call Die()
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Destroy the player
        Destroy(gameObject);
        EnemySpawner.Instance.players.Remove(this);
        if (EnemySpawner.Instance.players.Count == 0)
        {
            Debug.Log("Game Over!");
            EnemySpawner.Instance.enemies.ForEach(enemy => Destroy(enemy.gameObject));
            EnemySpawner.Instance.StopSpawning();
        }
    }
    public void AddAttack(Attack attack)
    {
        attacks.Add(attack);
        attack.Initialize(gameObject);
    }

    // Method to increase a player stat
    public void IncreaseStat(PlayerStat stat, int amount)
    {
        switch (stat)
        {
            case PlayerStat.Health:
                health += amount;
                break;
            case PlayerStat.Speed:
                movement.speed += amount;
                break;
        }
    }
}

public enum PlayerStat
{
    Health,
    Speed,
    // Add other player stats as needed
}
