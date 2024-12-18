using UnityEngine;
using System.Collections.Generic;

public enum StatType
{
    Damage,
    Speed,
    Pierce,
    ProjectileCount,
    Cooldown,
    Lifespan,
    RotationSpeed,
    MaxHomingDistance,
    Radius,
    // Add other stats as needed
}

[System.Serializable]
public class AttackStat
{
    public StatType statType;
    public float value;
}

public abstract class Attack : ScriptableObject
{
    public string attackName;
    public List<AttackStat> stats = new List<AttackStat>();

    public float lastUsedTime;
    public bool autoAim;

    public virtual void Initialize(GameObject owner) { lastUsedTime = Time.time; autoAim = owner.GetComponent<Player>().autoAttack; }
    public virtual void Use(GameObject owner) {  }

    public float GetStat(StatType statType)
    {
        AttackStat stat = stats.Find(s => s.statType == statType);
        return stat != null ? stat.value : 0f;
    }
}
