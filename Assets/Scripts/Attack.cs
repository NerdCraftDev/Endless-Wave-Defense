using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public string attackName;
    private bool autoAttack;
    public float lastUsedTime;
    public int damage;

    public virtual void Initialize(GameObject owner) { lastUsedTime = Time.time; }
    public virtual void Use(GameObject owner) {  }
}

public enum AttackStat
{
    Damage,
    Cooldown,
    Range,
    // Add other attack stats as needed
}
