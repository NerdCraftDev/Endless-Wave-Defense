using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerUpgrades playerUpgrades;
    public PlayerData data;
    public GameObject UpgradeGUI;
    public Vector2 Position => transform.position;

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
        foreach (var attack in data.attacks)
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
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Toggle autoAim
            foreach (var attack in data.attacks)
            {
                attack.autoAim = !attack.autoAim;
            }
        }
        if (data.xp >= data.xpToUpgrade && playerUpgrades.GetAvailableUpgrades().Count > 0)
        {
            ShowUpgradeOptions();
            data.xp -= data.xpToUpgrade;
            data.xpToUpgrade += data.xpNeededPerLevel;
        }
    }

    public void Move(Vector2 direction)
    {
        movement.Move(direction);
    }

    public void Attack() {
        foreach (var attack in data.attacks)
        {
            attack.Use(gameObject);
        }
    }

    public void ShowUpgradeOptions()
    {
        List<Upgrade> options = playerUpgrades.GetUpgradeOptions();
        if (options.Count > 0) { UpgradeGUI.SetActive(true); Time.timeScale = 0; }
        for (int i = 0; i < 3; i++) 
        {
            if (i >= options.Count) 
            {
                UpgradeGUI.transform.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            Upgrade option = options[i];
            Debug.Log(option.upgradeName);
            // Display upgrade details on the buttons
            Transform upgradeBtn = UpgradeGUI.transform.GetChild(options.IndexOf(option));
            upgradeBtn.GetComponentInChildren<TextMeshProUGUI>().text = $"{option.upgradeName}\n\n{option.description}";
            // upgradeBtn.GetComponent<Image>().sprite = option.icon;
            Button upgradeButton = upgradeBtn.GetComponent<Button>();
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(() => option.ApplyUpgrade(gameObject));
            upgradeButton.onClick.AddListener(() => UpgradeGUI.SetActive(false));
            upgradeButton.onClick.AddListener(() => Time.timeScale = 1);
        }
    }
    public void SetSpeed(float speed)
    {
        movement.SetSpeed(speed);
    }

    public void Damage(int damage)
    {
        // Reduce the player's health by damage
        data.health -= damage;
        // If the player's health is less than or equal to 0, call Die()
        if (data.health <= 0)
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
        data.attacks.Add(attack);
        attack.Initialize(gameObject);
    }

    // Method to increase a player stat
    public void IncreaseStat(PlayerStat stat, int amount)
    {
        switch (stat)
        {
            case PlayerStat.Health:
                data.health += amount;
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
