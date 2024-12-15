// Upgrade.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;

    public List<Upgrade> prerequisites;

    public enum UpgradeType
    {
        AddAttack,
        UpgradePlayerStat,
        UpgradeAttackStat
    }
    public UpgradeType upgradeType;

    // Conditional fields based on upgradeType

    // Fields for AddAttack upgrade
    [Header("Add Attack Settings")]
    public Attack newAttack;

    // Fields for UpgradePlayerStat upgrade
    [Header("Upgrade Player Stat Settings")]
    public PlayerStat playerStatToUpgrade;
    public int statIncreaseAmount;

    // Apply the upgrade effect to the player
    public void Apply(Player player)
    {
        switch (upgradeType)
        {
            case UpgradeType.AddAttack:
                if (newAttack != null)
                {
                    player.AddAttack(newAttack);
                }
                break;

            case UpgradeType.UpgradePlayerStat:
                player.IncreaseStat(playerStatToUpgrade, statIncreaseAmount);
                break;
        }
    }
}