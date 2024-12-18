using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public string attackName;
    public float lastUsedTime;
    public bool autoAim;
    public int damage;

    public virtual void Initialize(GameObject owner) { lastUsedTime = Time.time; autoAim = false; }
    public virtual void Use(GameObject owner) {  }
}

public enum AttackStat
{
    Damage,
    Cooldown,
    Range,
    // Add other attack stats as needed
}
