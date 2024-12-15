using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerUpgrades : MonoBehaviour
{
    public List<Upgrade> acquiredUpgrades = new List<Upgrade>();

    [Header("All Available Upgrades")]
    public List<Upgrade> allUpgrades;

    public void ShowUpgradeOptions()
    {
        List<Upgrade> availableUpgrades = GetAvailableUpgrades();

        // Randomly select 3 upgrades
        List<Upgrade> options = new List<Upgrade>();

        for (int i = 0; i < 3 && availableUpgrades.Count > 0; i++)
        {
            int index = Random.Range(0, availableUpgrades.Count);
            options.Add(availableUpgrades[index]);
            availableUpgrades.RemoveAt(index);
        }

        // Display options to the player (implement UI logic)
    }

    private List<Upgrade> GetAvailableUpgrades()
    {
        return allUpgrades.Where(upgrade => !acquiredUpgrades.Contains(upgrade)
            && ArePrerequisitesMet(upgrade)).ToList();
    }

    private bool ArePrerequisitesMet(Upgrade upgrade)
    {
        foreach (var prereq in upgrade.prerequisites)
        {
            if (!acquiredUpgrades.Contains(prereq))
                return false;
        }
        return true;
    }

    public void AcquireUpgrade(Upgrade upgrade)
    {
        if (!acquiredUpgrades.Contains(upgrade))
        {
            acquiredUpgrades.Add(upgrade);
            upgrade.Apply(GetComponent<Player>());
        }
    }
}