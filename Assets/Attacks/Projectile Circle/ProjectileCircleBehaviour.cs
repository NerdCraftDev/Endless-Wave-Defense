using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileCircleBehaviour : MonoBehaviour
{
    private Dictionary<StatType, float> stats = new Dictionary<StatType, float>();
    private Transform owner;
    private float angleStep;
    private float currentAngle;

    void Update()
    {
        // Calculate the new position based on the current angle
        float posX = owner.position.x + Mathf.Cos(currentAngle) * stats[StatType.Radius];
        float posY = owner.position.y + Mathf.Sin(currentAngle) * stats[StatType.Radius];
        transform.position = new Vector3(posX, posY, 0f);

        // Rotate the projectile around the owner
        currentAngle += stats[StatType.Speed] * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage((int)stats[StatType.Damage]);
        }
    }

    public void Initialize(Attack attack, Transform owner, int index, int totalProjectiles, PlayerData playerData)
    {
        foreach (StatType stat in attack.stats.Select(s => s.statType))
        {
            stats[stat] = attack.GetStat(stat) + playerData.GetStatValue(stat);
        }

        this.owner = owner;

        // Calculate the initial angle for this projectile
        angleStep = 2 * Mathf.PI / totalProjectiles;
        currentAngle = index * angleStep;
    }
}
