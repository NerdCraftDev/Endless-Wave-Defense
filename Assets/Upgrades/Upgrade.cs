// Upgrade.cs
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;
    public List<Upgrade> prerequisites = new List<Upgrade>();
    public bool reusable = false;

    public List<Upgrade> GetPrerequisites()
    {
        return prerequisites;
    }

    public virtual void ApplyUpgrade(GameObject target)
    {
        if (!reusable) target.GetComponent<PlayerUpgrades>().acquiredUpgrades.Add(this);
    }
}