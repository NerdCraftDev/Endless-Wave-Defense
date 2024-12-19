using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileShotBehaviour : MonoBehaviour
{
    private Vector3 direction;
    private Dictionary<StatType, float> stats = new Dictionary<StatType, float>();
    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        Vector2 targetDirection = FindClosestEnemyDirection(_transform, stats[StatType.MaxHomingDistance]);
        if (targetDirection != Vector2.zero)
        {
            float step = stats[StatType.RotationSpeed] * Time.deltaTime;
            Vector2 newDirection = Vector2.Lerp(_transform.right, targetDirection, step).normalized;
            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            _transform.position += (Vector3)newDirection * (Time.deltaTime * stats[StatType.Speed]);
        }
        else
        {
            _transform.position += direction * (Time.deltaTime * stats[StatType.Speed]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stats[StatType.Pierce] <= 0)
        {
            return;
        }

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage((int)stats[StatType.Damage]);
            stats[StatType.Pierce]--;
            if (stats[StatType.Pierce] <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Attack attack, Vector3 direction, PlayerData playerData)
    {
        foreach (StatType stat in attack.stats.Select(s => s.statType))
        {
            stats[stat] = attack.GetStat(stat) + playerData.GetStatValue(stat);
        }

        float lifespan = Mathf.Max(0.1f, stats[StatType.Lifespan]);
        Destroy(gameObject, lifespan);

        // Set the initial rotation to face the given direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        this.direction = direction;
    }

    public static Vector3 FindClosestEnemyDirection(Transform origin, float maxDistance = 100f)
    {
        Transform closest = null;
        float closestDistanceSqr = maxDistance * maxDistance;

        foreach (var enemy in EnemySpawner.Instance.enemies)
        {
            if (enemy != null)
            {
                float distanceSqr = (enemy.transform.position - origin.position).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closest = enemy.transform;
                }
            }
        }

        return closest != null ? (closest.position - origin.position).normalized : Vector2.zero;
    }
}
