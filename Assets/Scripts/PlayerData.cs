using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    public int health = 5;
    public int xp = 0;
    public int xpToUpgrade = 50;
    public int xpNeededPerLevel = 5;
    public List<Attack> attacks;

    public List<AttackStat> baseStats = new List<AttackStat>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetStatValue(StatType statType)
    {
        AttackStat stat = baseStats.Find(s => s.statType == statType);
        return stat != null ? stat.value : 0f;
    }
}